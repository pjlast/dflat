namespace AccountsAPI.Tests;

using System.Net;
using Accounts.APIV1;
using Accounts.Store;
using Microsoft.AspNetCore.Mvc.Testing;

public class TestAPI
{
    [Fact]
    public async Task TestAccountsEndpoints()
    {
        using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Create an account for Customer 1
        var payload = new CreateAccountBody { customerId = 1 };
        var result = await client.PostAsJsonAsync("/api/v1/accounts", payload).ConfigureAwait(true);
        var content = await result.Content.ReadFromJsonAsync<Account>().ConfigureAwait(true);

        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        Assert.Equal(1, content?.Id);
        Assert.Equal(1, content?.CustomerId);

        // Create an account for Customer 2
        payload.customerId = 2;
        result = await client
            .PostAsJsonAsync(new Uri("/api/v1/accounts"), payload)
            .ConfigureAwait(true);
        content = await result.Content.ReadFromJsonAsync<Account>().ConfigureAwait(true);

        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        Assert.Equal(2, content?.Id);
        Assert.Equal(2, content?.CustomerId);

        // Fetch all accounts and assert we have 2
        result = await client.GetAsync(new Uri("/api/v1/accounts")).ConfigureAwait(true);
        var accountList = await result
            .Content.ReadFromJsonAsync<List<Account>>()
            .ConfigureAwait(true);

        Assert.NotNull(accountList);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(2, accountList.Count);
        Assert.Contains(new Account { Id = 1, CustomerId = 1 }, accountList);

        // Fetch the second created account and assert it belongs to customer 2
        result = await client.GetAsync(new Uri("/api/v1/accounts/2")).ConfigureAwait(true);
        var account = await result.Content.ReadFromJsonAsync<Account>().ConfigureAwait(true);

        Assert.NotNull(account);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(2, account.CustomerId);

        // Fetch an account that does not exist and assert it returns a 404
        result = await client.GetAsync(new Uri("/api/v1/accounts/3")).ConfigureAwait(true);

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

        // Fetch all accounts for customer 1
#pragma warning disable CA2234
        result = await client.GetAsync("/api/v1/accounts?customerId=1").ConfigureAwait(true);
        accountList = await result.Content.ReadFromJsonAsync<List<Account>>().ConfigureAwait(true);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(accountList);
        Assert.Single(accountList);
        Assert.Equal(1, accountList[0].CustomerId);
    }
}
