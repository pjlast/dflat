import createClient from 'openapi-fetch';
import type { paths } from './schema';
import { ACCOUNTS_API_URL } from '$env/static/private';

export const newAccountsClient = () => {
	return createClient<paths>({ baseUrl: ACCOUNTS_API_URL });
};
