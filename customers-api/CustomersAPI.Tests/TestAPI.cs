namespace CustomersAPI.Tests;

using System.Net;
using Customers.APIV1;
using Customers.Store;
using Microsoft.AspNetCore.Mvc.Testing;

public class TestAPI
{
    [Fact]
    public async Task TestCustomersEndpoints()
    {
        using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Create Customer 1
        var payload = new CreateCustomerBody("John", "Jacobs");
        var result = await client
            .PostAsJsonAsync("/api/v1/customers", payload)
            .ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);

        var content = await result.Content.ReadFromJsonAsync<Customer>().ConfigureAwait(true);
        Assert.Equal(1, content?.Id);
        Assert.Equal("John", content?.FirstName);
        Assert.Equal("Jacobs", content?.LastName);
    }
}
