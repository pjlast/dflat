namespace Transactions.Store;

/// <summary>
/// A <c>Transaction</c> represents a monetary transaction for a specific
/// account.
/// </summary>
/// <param name="Id">ID of the transaction.</param>
/// <param name="AccountId">ID of the account the transaction belongs to.</param>
/// <param name="Amount">Monetary value of the transaction.</param>
public record Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int Amount { get; set; }
}

/// <summary>
/// Interface <c>IStore</c> describes an interface that allows creating,
/// fetching, and deleting a collection of <c>Transaction</c> records.
/// </summary>
public interface IStore
{
    /// <summary>
    /// Create and store a new <c>Transaction</c> for the provided account with
    /// the provided amount and return it.
    /// </summary>
    /// <param name="accountId">ID of the account the transaction should belong to.</param>
    /// <param name="amount">Value of the transaction.</param>
    /// <returns>A newly created <c>Transaction</c>.</returns>
    Transaction Create(int accountId, int amount);

    /// <summary>
    /// Fetch all <c>Transaction</c>s belonging to a specific account.
    /// </summary>
    /// <param name="accountId">ID of the account of which the transactions should be fetched.</param>
    /// <returns>All <c>Transaction</c>s belonging to the account.</returns>
    ICollection<Transaction> GetByAccountId(int accountId);

    /// <summary>
    /// Delete all <c>Transaction</c>s belonging to an account with the provided
    /// ID.
    /// </summary>
    /// <param name="accountId">ID of the account of which the transactions should be deleted.</param>
    void DeleteByAccountId(int accountId);
}

public class InMemoryStore : IStore
{
    private List<Transaction> _transactions;

    public InMemoryStore()
    {
        _transactions = new List<Transaction>();
    }

    public Transaction Create(int accountId, int amount)
    {
        var lastTransaction = _transactions.LastOrDefault();
        var nextId = lastTransaction is null ? 1 : lastTransaction.Id + 1;

        var newTransaction = new Transaction
        {
            Id = nextId,
            AccountId = accountId,
            Amount = amount,
        };
        _transactions = _transactions.Append(newTransaction).ToList();
        return newTransaction;
    }

    public ICollection<Transaction> GetByAccountId(int accountId)
    {
        return _transactions.FindAll(t => t.AccountId == accountId).ToList();
    }

    public void DeleteByAccountId(int accountId)
    {
        _transactions = _transactions.FindAll(t => t.AccountId != accountId);
    }
}
