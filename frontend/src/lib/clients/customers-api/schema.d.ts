/**
 * This file was auto-generated by openapi-typescript.
 * Do not make direct changes to the file.
 */

export interface paths {
	'/api/v1/customers': {
		parameters: {
			query?: never;
			header?: never;
			path?: never;
			cookie?: never;
		};
		/**
		 * Get all customers.
		 * @description Get all customers on the system.
		 */
		get: operations['GetAllCustomers'];
		/**
		 * Update a customer.
		 * @description Update an existing customer with a matching ID. Returns a 404 status code if the customer does not exist.
		 */
		put: operations['UpdateCustomer'];
		/**
		 * Create a new customer.
		 * @description Create a new customer with the provided first and last names.
		 */
		post: operations['CreateCustomer'];
		delete?: never;
		options?: never;
		head?: never;
		patch?: never;
		trace?: never;
	};
	'/api/v1/customers/{id}': {
		parameters: {
			query?: never;
			header?: never;
			path?: never;
			cookie?: never;
		};
		/**
		 * Fetch a customer by their ID.
		 * @description Fetch a customer with the provided ID. Returns a 404 status code if the customer does not exist.
		 */
		get: operations['GetCustomerById'];
		put?: never;
		post?: never;
		/**
		 * Delete the customer with the specified ID.
		 * @description Delete the customer with the specified ID.
		 */
		delete: operations['DeleteCustomerById'];
		options?: never;
		head?: never;
		patch?: never;
		trace?: never;
	};
}
export type webhooks = Record<string, never>;
export interface components {
	schemas: {
		CreateCustomerBody: {
			firstName: string;
			lastName: string;
		};
		Customer: {
			/** Format: int32 */
			id: number;
			firstName: string;
			lastName: string;
		};
	};
	responses: never;
	parameters: never;
	requestBodies: never;
	headers: never;
	pathItems: never;
}
export type $defs = Record<string, never>;
export interface operations {
	GetAllCustomers: {
		parameters: {
			query?: never;
			header?: never;
			path?: never;
			cookie?: never;
		};
		requestBody?: never;
		responses: {
			/** @description OK */
			200: {
				headers: {
					[name: string]: unknown;
				};
				content: {
					'application/json': components['schemas']['Customer'][];
				};
			};
		};
	};
	UpdateCustomer: {
		parameters: {
			query?: never;
			header?: never;
			path?: never;
			cookie?: never;
		};
		requestBody: {
			content: {
				'application/json': components['schemas']['Customer'];
			};
		};
		responses: {
			/** @description OK */
			200: {
				headers: {
					[name: string]: unknown;
				};
				content: {
					'application/json': components['schemas']['Customer'];
				};
			};
			/** @description Not Found */
			404: {
				headers: {
					[name: string]: unknown;
				};
				content?: never;
			};
		};
	};
	CreateCustomer: {
		parameters: {
			query?: never;
			header?: never;
			path?: never;
			cookie?: never;
		};
		requestBody: {
			content: {
				'application/json': components['schemas']['CreateCustomerBody'];
			};
		};
		responses: {
			/** @description Created */
			201: {
				headers: {
					[name: string]: unknown;
				};
				content: {
					'application/json': components['schemas']['Customer'];
				};
			};
		};
	};
	GetCustomerById: {
		parameters: {
			query?: never;
			header?: never;
			path: {
				id: number;
			};
			cookie?: never;
		};
		requestBody?: never;
		responses: {
			/** @description OK */
			200: {
				headers: {
					[name: string]: unknown;
				};
				content: {
					'application/json': components['schemas']['Customer'];
				};
			};
			/** @description Not Found */
			404: {
				headers: {
					[name: string]: unknown;
				};
				content?: never;
			};
		};
	};
	DeleteCustomerById: {
		parameters: {
			query?: never;
			header?: never;
			path: {
				id: number;
			};
			cookie?: never;
		};
		requestBody?: never;
		responses: {
			/** @description OK */
			200: {
				headers: {
					[name: string]: unknown;
				};
				content?: never;
			};
		};
	};
}
