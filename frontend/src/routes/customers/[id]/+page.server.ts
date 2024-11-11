import type { Actions } from './$types';
import { newCustomersClient } from '$lib/clients/customers-api/client';
import { error, fail } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';
import { newTransactionsClient } from '$lib/clients/transactions-api/client';
import { newAccountsClient } from '$lib/clients/accounts-api/client';

export const load: PageServerLoad = async ({ params }) => {
	const customerId = parseInt(params.id);
	const customersClient = newCustomersClient();
	const accountsClient = newAccountsClient();
	const transactionsClient = newTransactionsClient();

	const { data: customer, response } = await customersClient.GET('/api/v1/customers/{id}', {
		params: { path: { id: customerId } }
	});
	if (response.status !== 200 || customer === undefined) {
		error(404, {
			message: 'Customer not found'
		});
	}

	const { data: accounts, response: accountsResp } = await accountsClient.GET('/api/v1/accounts', {
		params: { query: { customerId: customer.id } }
	});
	if (accountsResp.status !== 200 || accounts === undefined) {
		error(500, {
			message: 'Could not fetch customer accounts'
		});
	}

	const accountsWithTransactions = await Promise.all(
		accounts.map(async (account) => {
			const { data: transactions, response: transactionsResp } = await transactionsClient.GET(
				'/api/v1/transactions',
				{ params: { query: { accountId: account.id } } }
			);
			if (transactionsResp.status !== 200 || transactions === undefined) {
				error(500, {
					message: 'Could not fetch account transactions'
				});
			}

			return {
				...account,
				transactions
			};
		})
	);

	return {
		customer,
		accounts: accountsWithTransactions
	};
};

export const actions = {
	default: async ({ params, request }) => {
		const customerId = parseInt(params.id);
		const data = await request.formData();
		const initialCreditForm = data.get('initialCredit');
		if (typeof initialCreditForm !== 'string') {
			return fail(400, { initialCreditForm, missing: true });
		}
		const initialCredit = parseInt(initialCreditForm);

		const accountsClient = newAccountsClient();
		const transactionsClient = newTransactionsClient();

		const { data: account, response } = await accountsClient.POST('/api/v1/accounts', {
			body: {
				customerId
			}
		});
		if (response.status !== 201 || account === undefined) {
			error(500, {
				message: 'Could not create customer account'
			});
		}

		if (initialCredit > 0) {
			const { response } = await transactionsClient.POST('/api/v1/transactions', {
				body: {
					accountId: account.id,
					amount: initialCredit
				}
			});
			if (response.status !== 201) {
				error(500, {
					message: 'Could not create initial transaction'
				});
			}
		}
	}
} satisfies Actions;
