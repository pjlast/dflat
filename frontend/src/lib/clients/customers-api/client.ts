import createClient from 'openapi-fetch';
import type { paths } from './schema';

export const newCustomersClient = () => {
	return createClient<paths>({ baseUrl: 'http://localhost:5295' });
};
