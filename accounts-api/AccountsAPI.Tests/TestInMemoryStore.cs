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

        var gotAccounts = store.GetAllAccounts();
        Assert.Equal(expectedAccounts, gotAccounts);
    }
}
