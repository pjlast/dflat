using Accounts.Store;

namespace AccountsAPI.Tests;

public class TestInMemoryStore
{
    IStore store;

    public TestInMemoryStore()
    {
        store = new InMemoryStore();
    }

    [Fact(DisplayName = "Create accounts for customer IDs")]
    public void CreateNewAccounts()
    {
        // Create accounts and assert that account Ids increment
        var account = store.Create(1);
        Assert.Equal(1, account.Id);
        Assert.Equal(1, account.CustomerId);

        account = store.Create(1);
        Assert.Equal(2, account.Id);
        Assert.Equal(1, account.CustomerId);

        account = store.Create(2);
        Assert.Equal(3, account.Id);
        Assert.Equal(2, account.CustomerId);
    }

    [Fact(DisplayName = "Fetch account by ID")]
    public void GetAccount()
    {
        var account = store.Create(10);

        var fetchedAccount = store.GetById(account.Id);
        Assert.NotNull(fetchedAccount);
        Assert.Equal(account.Id, fetchedAccount.Id);
        Assert.Equal(account.CustomerId, fetchedAccount.CustomerId);
    }

    [Fact(DisplayName = "Fetching an account that does not exist returns null")]
    public void GetNonExistentAccount()
    {
        var fetchedAccount = store.GetById(1337);
        Assert.Null(fetchedAccount);
    }

    [Fact(DisplayName = "Fetch all accounts")]
    public void GetAllAccounts()
    {
        var expectedAccounts = new List<Account>
        {
            store.Create(1),
            store.Create(2),
            store.Create(3),
        };

        var gotAccounts = store.GetAll();
        Assert.Equal(expectedAccounts, gotAccounts);
    }

    [Fact(DisplayName = "Fetch accounts by customer ID")]
    public void GetAccountsByCustomerId()
    {
        var expectedCustomer1Accounts = new List<Account> { store.Create(1), store.Create(1) };

        var expectedCustomer2Accounts = new List<Account> { store.Create(2) };

        var gotCustomer1Accounts = store.GetByCustomerId(1);
        Assert.Equal(expectedCustomer1Accounts, gotCustomer1Accounts);

        var gotCustomer2Accounts = store.GetByCustomerId(2);
        Assert.Equal(expectedCustomer2Accounts, gotCustomer2Accounts);
    }

    [Fact(DisplayName = "Delete an account")]
    public void DeleteAccount()
    {
        var account1 = store.Create(1);
        var account2 = store.Create(2);

        Assert.Equal(store.GetAll().Count, 2);

        store.DeleteById(account1.Id);

        Assert.Equal(store.GetAll().Count, 1);
        Assert.Null(store.GetById(account1.Id));
    }
}
