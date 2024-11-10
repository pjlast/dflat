namespace Customers.Store;

/// <summary>
/// A <c>Customer</c> is an actual customer on our system.
/// </summary>
/// <param name="Id">ID customer.</param>
/// <param name="firstName">First name.</param>
/// <param name="lastName">Last name.</param>
public record Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Customer(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
}

/// <summary>
/// Interface <c>IStore</c> describes an interface that allows creating,
/// fetching, updating, and deleting a collection of <c>Customer</c> records.
/// </summary>
public interface IStore
{
    /// <summary>
    /// Create and store a new <c>Customer</c>.
    /// </summary>
    /// <param name="firstName">First name.</param>
    /// <param name="lastName">Last name.</returns>
    Customer Create(string firstName, string lastName);
}

public class InMemoryStore : IStore
{
    private List<Customer> _customers;

    public InMemoryStore()
    {
        _customers = new List<Customer>();
    }

    public Customer Create(string firstName, string lastName)
    {
        var lastCustomer = _customers.LastOrDefault();
        var nextId = lastCustomer is null ? 1 : lastCustomer.Id + 1;

        var newCustomer = new Customer(nextId, firstName, lastName);
        _customers = _customers.Append(newCustomer).ToList();
        return newCustomer;
    }
}
