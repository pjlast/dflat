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

        // Fetch customer 2
#pragma warning disable CA2234
        result = await client.GetAsync("/api/v1/customers/2").ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var customer2 = await result.Content.ReadFromJsonAsync<Customer>().ConfigureAwait(true);

        Assert.NotNull(customer2);
        Assert.Equal(2, customer2.Id);

        // Fetching a non-existing customer returns 404
#pragma warning disable CA2234
        result = await client.GetAsync("/api/v1/customers/10").ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

        // Update a customer
        var customerToUpdate = new Customer(1, "Steve", "Stevenson");
        result = await client
            .PutAsJsonAsync("/api/v1/customers", customerToUpdate)
            .ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var updatedCustomer = await result
            .Content.ReadFromJsonAsync<Customer>()
            .ConfigureAwait(true);
        Assert.Equal(1, updatedCustomer?.Id);
        Assert.Equal("Steve", updatedCustomer?.FirstName);
        Assert.Equal("Stevenson", updatedCustomer?.LastName);

        // Delete a customer
#pragma warning disable CA2234
        result = await client.DeleteAsync("/api/v1/customers/1").ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

#pragma warning disable CA2234
        result = await client.GetAsync("/api/v1/customers").ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        customerList = await result
            .Content.ReadFromJsonAsync<List<Customer>>()
            .ConfigureAwait(true);

        Assert.NotNull(customerList);
        Assert.Single(customerList);
    }
}
