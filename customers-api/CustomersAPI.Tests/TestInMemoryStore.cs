using Customers.Store;

namespace CustomersAPI.Tests;

public class TestInMemoryStore
{
    InMemoryStore store;

    public TestInMemoryStore()
    {
        store = new InMemoryStore();
    }

    [Fact(DisplayName = "Create new customers")]
    public void CreateNewCustomers()
    {
        var customer = store.Create("Sally", "Smith");
        Assert.Equal(1, customer.Id);
        Assert.Equal("Sally", customer.FirstName);
        Assert.Equal("Smith", customer.LastName);

        customer = store.Create("John", "Jacobs");
        Assert.Equal(2, customer.Id);
        Assert.Equal("John", customer.FirstName);
        Assert.Equal("Jacobs", customer.LastName);

        customer = store.Create("Pete", "Peterson");
        Assert.Equal(3, customer.Id);
        Assert.Equal("Pete", customer.FirstName);
        Assert.Equal("Peterson", customer.LastName);
    }
}
