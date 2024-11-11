import createClient from 'openapi-fetch';
import type { paths } from './schema';
import { env } from '$env/dynamic/private';

export const newTransactionsClient = () => {
	return createClient<paths>({ baseUrl: env.TRANSACTIONS_API_URL });
};
