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
        var payload = new CreateAccountBody { customerId = 1 };

        var result = await client.PostAsJsonAsync("/api/v1/accounts", payload).ConfigureAwait(true);
        var content = await result.Content.ReadFromJsonAsync<Account>().ConfigureAwait(true);

        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        Assert.Equal(1, content?.Id);
        Assert.Equal(1, content?.CustomerId);

        result = await client.PostAsJsonAsync("/api/v1/accounts", payload).ConfigureAwait(true);
        content = await result.Content.ReadFromJsonAsync<Account>().ConfigureAwait(true);

        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        Assert.Equal(2, content?.Id);
        Assert.Equal(1, content?.CustomerId);
    }
}
