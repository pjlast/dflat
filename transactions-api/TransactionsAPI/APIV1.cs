using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Transactions.Store;

namespace Transactions.APIV1;

public static class TransactionsAPIV1
{
    static Created<Transaction> CreateTransaction(CreateTransactionBody body, IStore store)
    {
        var transaction = store.Create(body.accountId, body.amount);
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

        return group;
    }
}

public record CreateTransactionBody
{
    public int accountId { get; set; }
    public int amount { get; set; }
}
