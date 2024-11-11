import createClient from 'openapi-fetch';
import type { paths } from './schema';
import { CUSTOMERS_API_URL } from '$env/static/private';

export const newCustomersClient = () => {
    return createClient<paths>({ baseUrl: CUSTOMERS_API_URL });
};
