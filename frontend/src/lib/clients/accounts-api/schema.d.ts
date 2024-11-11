/**
 * This file was auto-generated by openapi-typescript.
 * Do not make direct changes to the file.
 */

export interface paths {
	'/api/v1/accounts': {
		parameters: {
			query?: never;
			header?: never;
			path?: never;
			cookie?: never;
		};
		/**
		 * Fetch all accounts.
		 * @description Fetches a list of all accounts that exist.
		 */
		get: operations['GetAccounts'];
		put?: never;
		/**
		 * Create a new account.
		 * @description Create a new account for a customer with the provided customer ID.
		 */
		post: operations['CreateAccount'];
		/**
		 * Delete all accounts belonging to the customer with the specified ID.
		 * @description Deletes all accounts belonging to the customer with the specified ID.
		 */
		delete: operations['DeleteAccountsByCustomerId'];
		options?: never;
		head?: never;
		patch?: never;
		trace?: never;
	};
	'/api/v1/accounts/{id}': {
		parameters: {
			query?: never;
			header?: never;
			path?: never;
			cookie?: never;
		};
		/**
		 * Fetch an account by its ID.
		 * @description Fetch an account with the provided ID. Returns a 404 status code if the account does not exist.
		 */
		get: operations['GetAccountById'];
		put?: never;
		post?: never;
		/**
		 * Delete an account with the specified ID.
		 * @description Delete an account with the provided ID. Returns a 404 status code if the account does not exist.
		 */
		delete: operations['DeleteAccountById'];
		options?: never;
		head?: never;
		patch?: never;
		trace?: never;
	};
}
export type webhooks = Record<string, never>;
export interface components {
	schemas: {
		Account: {
			/** Format: int32 */
			id: number;
			/** Format: int32 */
			customerId: number;
		};
		CreateAccountBody: {
			/** Format: int32 */
			customerId: number;
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
	GetAccounts: {
		parameters: {
			query?: {
				customerId?: number;
			};
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
					'application/json': components['schemas']['Account'][];
				};
			};
		};
	};
	CreateAccount: {
		parameters: {
			query?: never;
			header?: never;
			path?: never;
			cookie?: never;
		};
		requestBody: {
			content: {
				'application/json': components['schemas']['CreateAccountBody'];
			};
		};
		responses: {
			/** @description Created */
			201: {
				headers: {
					[name: string]: unknown;
				};
				content: {
					'application/json': components['schemas']['Account'];
				};
			};
		};
	};
	DeleteAccountsByCustomerId: {
		parameters: {
			query: {
				customerId: number;
			};
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
				content?: never;
			};
		};
	};
	GetAccountById: {
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
					'application/json': components['schemas']['Account'];
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
	DeleteAccountById: {
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
			/** @description Not Found */
			404: {
				headers: {
					[name: string]: unknown;
				};
				content?: never;
			};
		};
	};
}
