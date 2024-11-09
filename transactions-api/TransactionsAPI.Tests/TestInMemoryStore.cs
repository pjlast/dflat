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

    [Fact(DisplayName = "Fetch all transactions for a specific account")]
    public void GetTransactionsByAccountId()
    {
        var expectedAccount1Transactions = new List<Transaction>
        {
            store.Create(1, 10),
            store.Create(1, 20),
        };

        var expectedAccount2Transactions = new List<Transaction> { store.Create(2, 30) };

        var gotAccount1Transactions = store.GetByAccountId(1);
        Assert.Equal(expectedAccount1Transactions, gotAccount1Transactions);

        var gotAccount2Transactions = store.GetByAccountId(2);
        Assert.Equal(expectedAccount2Transactions, gotAccount2Transactions);
    }

    [Fact(DisplayName = "Delete all transactions for a specific account")]
    public void DeleteTransactionsByAccountId()
    {
        store.Create(1, 10);
        store.Create(1, 20);
        store.Create(2, 30);

        Assert.Equal(2, store.GetByAccountId(1).Count);
        Assert.Single(store.GetByAccountId(2));

        store.DeleteByAccountId(1);

        Assert.Single(store.GetByAccountId(2));
        Assert.Empty(store.GetByAccountId(1));
    }
}
