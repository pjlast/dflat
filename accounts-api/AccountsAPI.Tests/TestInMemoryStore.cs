using Accounts.Store;

namespace AccountsAPI.Tests;

public class TestInMemoryStore
{
    [Fact(DisplayName = "Create accounts for customer IDs")]
    public void CreateNewAccounts()
    {
        var store = new InMemoryStore();

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
}
