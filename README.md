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
