using Accounts.Store;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Accounts.APIV1;

public static class AccountsAPIV1
{
    static Created<Account> CreateAccount(CreateAccountBody body, IStore store)
    {
        var account = store.Create(body.customerId);
        return TypedResults.Created($"/api/v1/accounts/{account.Id}", account);
    }

    static Ok<ICollection<Account>> GetAccounts(IStore store)
    {
        var accounts = store.GetAll();
        return TypedResults.Ok(accounts);
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
        return group;
    }
}

public record CreateAccountBody
{
    public int customerId { get; set; }
}
