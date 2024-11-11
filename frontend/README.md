# Frontend

`frontend` is a SvelteKit app that exposes a UI (available at the root path `/`) as well as an API (available at `/api/v1`).

## Setup

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
