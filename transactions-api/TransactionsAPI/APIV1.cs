using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Transactions.Store;

namespace Transactions.APIV1;

public static class TransactionsAPIV1
{
    static Created<Transaction> CreateTransaction(CreateTransactionBody body, IStore store)
    {
        var transaction = store.Create(body.AccountId, body.Amount);
        return TypedResults.Created($"/api/v1/transactions/{transaction.Id}", transaction);
    }

    static Ok<ICollection<Transaction>> GetTransactionsByAccountId(
        [FromQuery(Name = "accountId")] int accountId,
        IStore store
    )
    {
        var transactions = store.GetByAccountId(accountId);
        return TypedResults.Ok(transactions);
    }

    static Ok DeleteTransactionsByAccountId(
        [FromQuery(Name = "accountId")] int accountId,
        IStore store
    )
    {
        store.DeleteByAccountId(accountId);
        return TypedResults.Ok();
    }

    public static RouteGroupBuilder MapTransactionsAPI(this RouteGroupBuilder group)
    {
        group
            .MapPost("/", CreateTransaction)
            .WithName("CreateTransaction")
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Summary = "Create a new transaction.";
                generatedOperation.Description =
                    "Create a new transaction for an account with the provided amount.";

                return generatedOperation;
            });

        group
            .MapGet("/", GetTransactionsByAccountId)
            .WithName("GetTransactionsByAccountId")
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Summary = "Fetch all transactions belonging to an account.";
                generatedOperation.Description =
                    "Fetch all transactions that belong to the account with the provided ID.";
                return generatedOperation;
            });

        group
            .MapDelete("/", DeleteTransactionsByAccountId)
            .WithName("DeleteTransactionsByAccountId")
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Summary = "Delete all transactions belonging to an account.";
                generatedOperation.Description =
                    "Delete all transactions that belong to the account with the provided ID.";
                return generatedOperation;
            });

        return group;
    }
}

public record CreateTransactionBody
{
    public int AccountId { get; set; }
    public int Amount { get; set; }
}
