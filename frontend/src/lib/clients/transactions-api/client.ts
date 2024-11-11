import createClient from 'openapi-fetch';
import type { paths } from './schema';
import { TRANSACTIONS_API_URL } from '$env/static/private';

export const newTransactionsClient = () => {
	return createClient<paths>({ baseUrl: TRANSACTIONS_API_URL });
};
