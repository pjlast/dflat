using Accounts.Store;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.APIV1;

public static class AccountsAPIV1
{
    static Created<Account> CreateAccount(CreateAccountBody body, IStore store)
    {
        var account = store.Create(body.customerId);
        return TypedResults.Created($"/api/v1/accounts/{account.Id}", account);
    }

    static Ok<ICollection<Account>> GetAccounts(
        [FromQuery(Name = "customerId")] int? customerId,
        IStore store
    )
    {
        if (customerId is int id)
        {
            var accounts = store.GetByCustomerId(id);
            return TypedResults.Ok(accounts);
        }
        else
        {
            var accounts = store.GetAll();
            return TypedResults.Ok(accounts);
        }
    }

    static IResult GetAccountById(int id, IStore store)
    {
        var account = store.GetById(id);
        if (account is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(account);
    }

    public static RouteGroupBuilder MapAccountsAPI(this RouteGroupBuilder group)
    {
        group
            .MapPost("/", CreateAccount)
            .WithName("CreateAccount")
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Summary = "Create a new account.";
                generatedOperation.Description =
                    "Create a new account for a customer with the provided customer ID.";

                return generatedOperation;
            });

        group
            .MapGet("/", GetAccounts)
            .WithName("GetAccounts")
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Summary = "Fetch all accounts.";
                generatedOperation.Description = "Fetches a list of all accounts that exist.";

                return generatedOperation;
            });

        group
            .MapGet("/{id}", GetAccountById)
            .WithName("GetAccountById")
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Summary = "Fetch an account by its ID.";
                generatedOperation.Description =
                    "Fetch an account with the provided ID. Returns a 404 status code if the account does not exist.";

                return generatedOperation;
            })
            .Produces<Account>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return group;
    }
}

public record CreateAccountBody
{
    public int customerId { get; set; }
}
