# Transactions API

`transactions-api` manages transaction details.

## Dev

To get up and running:

```
dotnet restore
dotnet tool restore
dotnet build
```

Running `dotnet build` will also update the `openapi.json` file.

To run the project:

```
dotnet run
```

A Swagger API explorer is available at `/swagger`.

To run tests:

```
dotnet test
```

Code formatting is done via `csharpier`:

```
dotnet csharpier .
```
