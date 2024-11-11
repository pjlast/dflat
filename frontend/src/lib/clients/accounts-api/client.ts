import createClient from 'openapi-fetch';
import type { paths } from './schema';

export const newAccountsClient = () => {
	return createClient<paths>({ baseUrl: 'http://localhost:5018' });
};
