namespace TransactionsAPI.Tests;

using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Transactions.APIV1;
using Transactions.Store;

public class TestAPI
{
    [Fact]
    public async Task TestTransactionsEndpoints()
    {
        using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Create a transaction for Account 1
        var payload = new CreateTransactionBody { accountId = 1, amount = 10 };
        var result = await client
            .PostAsJsonAsync("/api/v1/transactions", payload)
            .ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);

        var content = await result.Content.ReadFromJsonAsync<Transaction>().ConfigureAwait(true);

        Assert.Equal(1, content?.Id);
        Assert.Equal(1, content?.AccountId);
        Assert.Equal(10, content?.Amount);

        // Create a transaction for Account 2
        payload = new CreateTransactionBody { accountId = 2, amount = 20 };
        result = await client.PostAsJsonAsync("/api/v1/transactions", payload).ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);

        content = await result.Content.ReadFromJsonAsync<Transaction>().ConfigureAwait(true);

        Assert.Equal(2, content?.Id);
        Assert.Equal(2, content?.AccountId);
        Assert.Equal(20, content?.Amount);

        // Fetch transactions for Account 2
#pragma warning disable CA2234
        result = await client.GetAsync("/api/v1/transactions?accountId=2").ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var transactionList = await result
            .Content.ReadFromJsonAsync<List<Transaction>>()
            .ConfigureAwait(true);

        Assert.NotNull(transactionList);
        Assert.Single(transactionList);
        Assert.Equal(
            new Transaction
            {
                Id = 2,
                AccountId = 2,
                Amount = 20,
            },
            transactionList[0]
        );

        // Delete transactions for Account 2
#pragma warning disable CA2234
        result = await client.DeleteAsync("/api/v1/transactions?accountId=2").ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

#pragma warning disable CA2234
        result = await client.GetAsync("/api/v1/transactions?accountId=2").ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        transactionList = await result
            .Content.ReadFromJsonAsync<List<Transaction>>()
            .ConfigureAwait(true);

        Assert.NotNull(transactionList);
        Assert.Empty(transactionList);

        // Assert Account 1's transactions still exist
#pragma warning disable CA2234
        result = await client.GetAsync("/api/v1/transactions?accountId=1").ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        transactionList = await result
            .Content.ReadFromJsonAsync<List<Transaction>>()
            .ConfigureAwait(true);

        Assert.NotNull(transactionList);
        Assert.Single(transactionList);
        Assert.Equal(
            new Transaction
            {
                Id = 1,
                AccountId = 1,
                Amount = 10,
            },
            transactionList[0]
        );
    }
}
