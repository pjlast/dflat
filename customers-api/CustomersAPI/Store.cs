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
    /// <returns>The newly created customer.</returns>
    Customer Create(string firstName, string lastName);

    /// <summary>
    /// Fetch a <c>Customer</c> with the provided ID.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>The matching customer, or null if it does not exist.</returns>
    Customer? GetById(int id);

    /// <summary>
    /// Fetch all <c>Customer</c>s in the store.
    /// </summary>
    /// <returns>A colleciton of all <c>Customer</c>s in the store.</returns>
    ICollection<Customer> GetAll();

    /// <summary>
    /// Update an existing <c>Customer</c>.
    /// </summary>
    /// <param name="customer">The customer to be updated. A customer with a matching <c>Id</c> must exist.</param>
    /// <returns>The updated <c>Customer</c>.</returns>
    /// <exception cref="KeyNotFoundException">No customer with a matching <c>Id</c> could be found.</exception>
    Customer Update(Customer customer);
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

    public Customer? GetById(int id)
    {
        return _customers.Find(c => c.Id == id);
    }

    public ICollection<Customer> GetAll()
    {
        return new List<Customer>(_customers);
    }

    public Customer Update(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        var existingCustomer = GetById(customer.Id);
        if (existingCustomer is null)
        {
            throw new KeyNotFoundException($"No existing customer with ID {customer.Id} found.");
        }

        _customers = _customers
            .Select(c =>
            {
                if (c.Id == customer.Id)
                {
                    return customer;
                }
                return c;
            })
            .ToList();

        return customer;
    }
}
