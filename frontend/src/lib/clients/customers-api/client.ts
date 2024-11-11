import createClient from 'openapi-fetch';
import type { paths } from './schema';
import { env } from '$env/dynamic/private';

export const newCustomersClient = () => {
	return createClient<paths>({ baseUrl: env.CUSTOMERS_API_URL });
};
