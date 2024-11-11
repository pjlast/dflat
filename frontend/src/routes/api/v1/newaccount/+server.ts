import { error, json, type RequestHandler } from '@sveltejs/kit';
import { newAccountsClient } from '$lib/clients/accounts-api/client';
import { newTransactionsClient } from '$lib/clients/transactions-api/client';

export const POST: RequestHandler = async ({ request }) => {
	const { customerId, initialCredit } = await request.json();
	if (typeof customerId !== 'number' || typeof initialCredit !== 'number') {
		error(400);
	}

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

	let transactions = [];
	if (initialCredit > 0) {
		const { data: transaction, response } = await transactionsClient.POST('/api/v1/transactions', {
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
		transactions.push(transaction);
	}

	return json(
		{
			...account,
			transactions
		},
		{ status: 201 }
	);
};
