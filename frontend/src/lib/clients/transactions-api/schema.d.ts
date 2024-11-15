/**
 * This file was auto-generated by openapi-typescript.
 * Do not make direct changes to the file.
 */

export interface paths {
	'/api/v1/transactions': {
		parameters: {
			query?: never;
			header?: never;
			path?: never;
			cookie?: never;
		};
		/**
		 * Fetch all transactions belonging to an account.
		 * @description Fetch all transactions that belong to the account with the provided ID.
		 */
		get: operations['GetTransactionsByAccountId'];
		put?: never;
		/**
		 * Create a new transaction.
		 * @description Create a new transaction for an account with the provided amount.
		 */
		post: operations['CreateTransaction'];
		/**
		 * Delete all transactions belonging to an account.
		 * @description Delete all transactions that belong to the account with the provided ID.
		 */
		delete: operations['DeleteTransactionsByAccountId'];
		options?: never;
		head?: never;
		patch?: never;
		trace?: never;
	};
}
export type webhooks = Record<string, never>;
export interface components {
	schemas: {
		CreateTransactionBody: {
			/** Format: int32 */
			accountId: number;
			/** Format: int32 */
			amount: number;
		};
		Transaction: {
			/** Format: int32 */
			id: number;
			/** Format: int32 */
			accountId: number;
			/** Format: int32 */
			amount: number;
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
	GetTransactionsByAccountId: {
		parameters: {
			query: {
				accountId: number;
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
					'application/json': components['schemas']['Transaction'][];
				};
			};
		};
	};
	CreateTransaction: {
		parameters: {
			query?: never;
			header?: never;
			path?: never;
			cookie?: never;
		};
		requestBody: {
			content: {
				'application/json': components['schemas']['CreateTransactionBody'];
			};
		};
		responses: {
			/** @description Created */
			201: {
				headers: {
					[name: string]: unknown;
				};
				content: {
					'application/json': components['schemas']['Transaction'];
				};
			};
		};
	};
	DeleteTransactionsByAccountId: {
		parameters: {
			query: {
				accountId: number;
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
}
