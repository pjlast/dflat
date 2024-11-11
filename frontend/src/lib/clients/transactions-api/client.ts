import createClient from 'openapi-fetch';
import type { paths } from './schema';

export const newTransactionsClient = () => {
	return createClient<paths>({ baseUrl: 'http://localhost:5009' });
};
