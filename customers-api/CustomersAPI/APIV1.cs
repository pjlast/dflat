using Customers.Store;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.APIV1;

public static class CustomersAPIV1
{
    static Created<Customer> CreateCustomer(CreateCustomerBody body, IStore store)
    {
        var customer = store.Create(body.FirstName, body.LastName);
        return TypedResults.Created($"/api/v1/customers/{customer.Id}", customer);
    }

    static Ok<ICollection<Customer>> GetCustomers(IStore store)
    {
        return TypedResults.Ok(store.GetAll());
    }

    public static RouteGroupBuilder MapCustomersAPI(this RouteGroupBuilder group)
    {
        group
            .MapPost("/", CreateCustomer)
            .WithName("CreateCustomer")
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Summary = "Create a new customer.";
                generatedOperation.Description =
                    "Create a new customer with the provided first and last names.";
                return generatedOperation;
            });

        group.MapGet("/", GetCustomers)
            .WithName("GetAllCustomers")
            .WithOpenApi(generatedOperation =>
                    {
                        generatedOperation.Summary = "Get all customers.";
                        generatedOperation.Description = "Get all customers on the system.";
                        return generatedOperation;
                    });
        return group;
    }
}

public record CreateCustomerBody
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public CreateCustomerBody(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
