import { error, json, type RequestHandler } from '@sveltejs/kit';
import { newAccountsClient } from '$lib/clients/accounts-api/client';
import { newTransactionsClient } from '$lib/clients/transactions-api/client';
import { newCustomersClient } from '$lib/clients/customers-api/client';

export const GET: RequestHandler = async ({ params }) => {
	const customerId = parseInt(params.id as string);

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

	return json(
		{
			...customer,
			accounts: accountsWithTransactions
		},
		{ status: 200 }
	);
};
