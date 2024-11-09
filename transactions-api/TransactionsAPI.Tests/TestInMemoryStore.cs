using Transactions.Store;

namespace TransactionsAPI.Tests;

public class TestInMemoryStore
{
    InMemoryStore store;

    public TestInMemoryStore()
    {
        store = new InMemoryStore();
    }

    [Fact(DisplayName = "Create transactions for account IDs")]
    public void CreateNewTransactions()
    {
        var transaction = store.Create(1, 10);
        Assert.Equal(1, transaction.Id);
        Assert.Equal(1, transaction.AccountId);
        Assert.Equal(10, transaction.Amount);

        transaction = store.Create(1, 20);
        Assert.Equal(2, transaction.Id);
        Assert.Equal(1, transaction.AccountId);
        Assert.Equal(20, transaction.Amount);

        transaction = store.Create(2, 10);
        Assert.Equal(3, transaction.Id);
        Assert.Equal(2, transaction.AccountId);
        Assert.Equal(10, transaction.Amount);
    }
}
