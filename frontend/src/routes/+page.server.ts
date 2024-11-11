import type { PageServerLoad } from './$types';
import { error } from '@sveltejs/kit';
import { newCustomersClient } from '$lib/clients/customers-api/client';
import { newAccountsClient } from '$lib/clients/accounts-api/client';
import { newTransactionsClient } from '$lib/clients/transactions-api/client';

export const load: PageServerLoad = async () => {
	const customersClient = newCustomersClient();
	const accountsClient = newAccountsClient();
	const transactionsClient = newTransactionsClient();

	const { data, response } = await customersClient.GET('/api/v1/customers');

	if (data === undefined || response.status != 200) {
		error(500, { message: 'Could not fetch customers' });
	}

	const customersWithBalance = await Promise.all(
		data.map(async (customer) => {
			const { data: accounts, response: accountsResp } = await accountsClient.GET(
				'/api/v1/accounts',
				{
					params: {
						query: {
							customerId: customer.id
						}
					}
				}
			);
			if (accountsResp.status != 200 || accounts === undefined) {
				error(500, { message: 'Could not fetch customer accounts' });
			}

			let balance = 0;
			for (const account of accounts) {
				const { data: transactions, response: transactionsResp } = await transactionsClient.GET(
					'/api/v1/transactions',
					{
						params: {
							query: {
								accountId: account.id
							}
						}
					}
				);
				if (transactionsResp.status != 200 || transactions === undefined) {
					error(500, { message: 'Could not fetch account transactions' });
				}

				balance = balance + transactions?.reduce((acc, transaction) => acc + transaction.amount, 0);
			}

			return {
				...customer,
				balance
			};
		})
	);

	return {
		customers: customersWithBalance
	};
};
