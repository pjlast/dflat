namespace Accounts.Store;

public record Account
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
}

public interface IStore
{
    Account Create(int customerId);
    Account? GetById(int id);
    ICollection<Account> GetAll();
    ICollection<Account> GetByCustomerId(int customerId);
    void DeleteById(int id);
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
            throw new KeyNotFoundException($"account with ID {id} does not exit");
        }
        _accounts = _accounts.FindAll(a => a.Id != id);
    }

    public void DeleteByCustomerId(int customerId)
    {
        _accounts = _accounts.FindAll(a => a.CustomerId != customerId);
    }
}
