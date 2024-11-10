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

    static IResult GetCustomerById(int id, IStore store)
    {
        var customer = store.GetById(id);
        if (customer is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(customer);
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

        group
            .MapGet("/", GetCustomers)
            .WithName("GetAllCustomers")
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Summary = "Get all customers.";
                generatedOperation.Description = "Get all customers on the system.";
                return generatedOperation;
            });

        group
            .MapGet("/{id}", GetCustomerById)
            .WithName("GetCustomerById")
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Summary = "Fetch a customer by their ID.";
                generatedOperation.Description =
                    "Fetch a customer with the provided ID. Returns a 404 status code if the customer does not exist.";

                return generatedOperation;
            })
            .Produces<Customer>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

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
