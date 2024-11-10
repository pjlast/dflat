namespace Accounts.Store;

/// <summary>
/// An <c>Account</c> represents a transactional account of a specific
/// customer.
/// </summary>
/// <param name="Id">ID of the account.</param>
/// <param name="CustomerId">ID of the customer the account belongs to.</param>
public record Account
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
}

/// <summary>
/// Interface <c>IStore</c> describes an interface that allows creating,
/// deleting, and fetching a collection of <c>Account</c> records.
/// </summary>
public interface IStore
{
    /// <summary>
    /// Create and store a new <c>Account</c> for a customer with the provided
    /// ID and return it.
    /// </summary>
    /// <param name="customerId">ID of the customer the account should belong to.</param>
    /// <returns>A newly created <c>Account</c>.</returns>
    Account Create(int customerId);

    /// <summary>
    /// Fetch an <c>Account</c> with the provided ID and return it if it
    /// exists. Returns <c>null</c> if no account could be found.
    /// </summary>
    /// <param name="id">ID of the account to fetch.</param>
    /// <returns>The corresponding <c>Account</c> or <c>null</c>.</returns>
    Account? GetById(int id);

    /// <summary>
    /// Fetch all <c>Account</c>s that have been stored.
    /// </summary>
    /// <returns>All <c>Account</c>s in the store.</returns>
    ICollection<Account> GetAll();

    /// <summary>
    /// Fetch all <c>Account</c>s belonging to a specific customer.
    /// </summary>
    /// <param name="customerId">ID of the customer whose accounts to fetch.</param>
    /// <returns>All <c>Account</c>s belonging to the customer.</returns>
    ICollection<Account> GetByCustomerId(int customerId);

    /// <summary>
    /// Delete an <c>Account</c> with the given ID. Throws a <c>KeyNotFoundException</c>
    /// if no account with the given ID could be found.
    /// </summary>
    /// <param name="id">ID of the count that should be deleted.</param>
    /// <exception cref="KeyNotFoundException">An account with the provided ID could not be found.</exception>
    void DeleteById(int id);

    /// <summary>
    /// Delete all <c>Account</c>s belonging to a customer with the provided
    /// ID.
    /// </summary>
    /// <param name="customerId">ID of the customer whose accounts should be deleted.</param>
    void DeleteByCustomerId(int customerId);
}

public class InMemoryStore : IStore
{
    private List<Account> _accounts;

    public InMemoryStore()
    {
        _accounts = new List<Account>();
    }

    public Account Create(int customerId)
    {
        var lastAccount = _accounts.LastOrDefault();
        var nextId = lastAccount is null ? 1 : lastAccount.Id + 1;

        var newAccount = new Account { Id = nextId, CustomerId = customerId };
        _accounts = _accounts.Append(newAccount).ToList();
        return newAccount;
    }

    public Account? GetById(int id)
    {
        return _accounts.Find(a => a.Id == id);
    }

    public ICollection<Account> GetAll()
    {
        return new List<Account>(_accounts);
    }

    public ICollection<Account> GetByCustomerId(int customerId)
    {
        return _accounts.FindAll(a => a.CustomerId == customerId).ToList();
    }

    public void DeleteById(int id)
    {
        var account = _accounts.Find(a => a.Id == id);
        if (account is null)
        {
            throw new KeyNotFoundException($"account with ID {id} does not exist");
        }
        _accounts = _accounts.FindAll(a => a.Id != id);
    }

    public void DeleteByCustomerId(int customerId)
    {
        _accounts = _accounts.FindAll(a => a.CustomerId != customerId);
    }
}
