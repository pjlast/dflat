import createClient from 'openapi-fetch';
import type { paths } from './schema';
import { env } from '$env/dynamic/private';

export const newAccountsClient = () => {
	return createClient<paths>({ baseUrl: env.ACCOUNTS_API_URL });
};
