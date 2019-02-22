using System.Collections.Generic;

namespace IdleMachinery.Extensions.Standard.Stubs.Domain
{
    public interface ICustomerData
    {
        IEnumerable<Customer> GetCustomersByName(string name = null);
        Customer GetById(int id);
        Customer Update(Customer updatedCustomer);
        Customer Add(Customer newCustomer);
        Customer Delete(int id);
        int Count();
        int Commit();
    }
}
