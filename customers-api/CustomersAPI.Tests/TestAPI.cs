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

        // Create Customer 2
        payload = new CreateCustomerBody("Betty", "Burkes");
        result = await client.PostAsJsonAsync("/api/v1/customers", payload).ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);

        content = await result.Content.ReadFromJsonAsync<Customer>().ConfigureAwait(true);
        Assert.Equal(2, content?.Id);
        Assert.Equal("Betty", content?.FirstName);
        Assert.Equal("Burkes", content?.LastName);

        // Fetch all customers
#pragma warning disable CA2234
        result = await client.GetAsync("/api/v1/customers").ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var customerList = await result
            .Content.ReadFromJsonAsync<List<Customer>>()
            .ConfigureAwait(true);

        Assert.NotNull(customerList);
        Assert.Equal(2, customerList.Count);
    }
}
