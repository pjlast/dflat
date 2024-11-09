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
    }
}
