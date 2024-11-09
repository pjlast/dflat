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
    List<Account> GetAllAccounts();
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

    public List<Account> GetAllAccounts()
    {
        return _accounts;
    }
}
