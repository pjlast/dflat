# Frontend

`frontend` is a SvelteKit app that exposes a UI (available at the root path `/`) as well as an API (available at `/api/v1`).

## Dev

The `frontend` service requires `customers-api`, `accounts-api` and `transactions-api` to be running, and you need to provide the service with their URLs.

You can do so by creating a `.env` file (adjust the URLs as needed):

```
CUSTOMERS_API_URL="http://localhost:5295"
ACCOUNTS_API_URL="http://localhost:5018"
TRANSACTIONS_API_URL="http://localhost:5009"
```

```
pnpm i
pnpm run dev
```

To format files:

```
pnpm run format
```

To regenerate OpenAPI clients:

```
pnpm run generate
```

## API

The API is available at `/api/v1` and exposes the following methods:


```
// Open a new account for a customer
POST /api/v1/newaccount
{
    "customerId": "number",
    "initialCredit": "number"
}

// Fetch customer details
GET /api/v1/customers/:id
```
