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

    [Fact(DisplayName = "Fetch customer by ID")]
    public void GetCustomerByID()
    {
        var expectedCustomer = store.Create("Sally", "Smith");
        var gotCustomer = store.GetById(expectedCustomer.Id);
        Assert.Equal(expectedCustomer, gotCustomer);
    }

    [Fact(DisplayName = "Fetch all customers")]
    public void GetAllCustomers()
    {
        var expectedCustomers = new List<Customer>
        {
            store.Create("Sally", "Smith"),
            store.Create("John", "Jacobs"),
        };

        var gotCustomers = store.GetAll();
        Assert.Equal(expectedCustomers, gotCustomers);
    }

    [Fact(DisplayName = "Update a customer's details")]
    public void UpdateCustomer()
    {
        var existingCustomer = store.Create("John", "Jacobss");

        var updateCustomer = new Customer(existingCustomer.Id, "John", "Jacobs");
        var updatedCustomer = store.Update(updateCustomer);

        var fetchedCustomer = store.GetById(updatedCustomer.Id);

        Assert.NotNull(fetchedCustomer);
        Assert.Equal(updatedCustomer, fetchedCustomer);
        Assert.Equal("Jacobs", fetchedCustomer.LastName);
    }

    [Fact(DisplayName = "Attempting to update a non-existent customer throws")]
    public void UpdateNonExistentCustomer()
    {
        var nonExistingCustomer = new Customer(10, "Barry", "Burkes");
        Assert.Throws<KeyNotFoundException>(() => store.Update(nonExistingCustomer));
    }
}
