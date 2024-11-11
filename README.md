# D♭

`dflat` is a repository showcasing a microservice setup using C# .NET backends with a SvelteKit frontend.

## Run

The easiest way to get up and running is using `docker compose`.

```
docker compose build
docker compose up
```

And the service should be available at `http://localhost:8080`.

## UI and API

The frontend service exposes a web UI as well as an API.

### UI

The web UI is available at the root.

### API

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

## Project setup

The project follows a microservice architecture with 4 services:

- `frontend`
- `customers-api`
- `accounts-api`
- `transactions-api`

`frontend` is a TypeScript SvelteKit app that serves a web UI as well as a JSON API, and interacts with the other 3 APIs to serve user requests.

The three `*-api` services are C# .NET minimal web REST APIs.

The entire project can be run locally using `docker compose`:

```
docker compose build
docker compose up
```

### Type safety

Each backend service produces an `openapi.json` file when built. The `frontend` generates TypeScript clients using these files, which is then used on the SvelteKit server to make requests to each service. This ensures that requests are well-formed, and if a service's API ever changes, the `frontend` service where the API is used should fail to build.

### CI/CD

Project wellness is enforced by some GitHub Actions that run when PRs are made to `main` (and on `main` itself). The `main` branch is protected, so no directed pushes can be made to `main`. Everything must happen through Pull Requests.

The actions check the following:

- Formatting
- Linting
- All tests pass
- Ensure projects build
- Ensures the `openapi.json` file is up to date for each service
