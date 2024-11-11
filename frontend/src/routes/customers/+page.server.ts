import type { Actions } from './$types';
import { fail, redirect } from '@sveltejs/kit';
import { newCustomersClient } from '$lib/clients/customers-api/client';

export const actions = {
	default: async ({ request }) => {
		const client = newCustomersClient();

		const data = await request.formData();
		const firstName = data.get('firstName');
		const lastName = data.get('lastName');
		if (typeof firstName !== 'string') {
			return fail(400, { firstName, missing: true });
		}
		if (typeof lastName !== 'string') {
			return fail(400, { lastName, missing: true });
		}

		const { response } = await client.POST('/api/v1/customers', {
			body: {
				firstName,
				lastName
			}
		});

		if (response.status === 201) {
			throw redirect(303, '/');
		} else {
			return fail(500, { message: 'User could not be created' });
		}
	}
} satisfies Actions;
