using System;
using System.Collections.Generic;
using System.Linq;

namespace IdleMachinery.Extensions.Standard.Stubs.Domain
{
    public class InMemoryCustomerData : ICustomerData
    {
        private readonly IList<Customer> customers;

        public InMemoryCustomerData()
        {
            customers = new List<Customer>()
            {
                new Customer { Id = 1, FirstName = "Elton", LastName = "Stoneman", Audit = new Audit("seed", DateTime.Now, DateTime.Now) },
                new Customer { Id = 2, FirstName = "Bill", LastName = "Sprigs", Audit = new Audit("seed", DateTime.Now, DateTime.Now) },
                new Customer { Id = 3, FirstName = "Susie", LastName = "Franklin", Audit = new Audit("seed", DateTime.Now, DateTime.Now) }
            };
        }

        public Customer Add(Customer newCustomer)
        {
            customers.Add(newCustomer);
            newCustomer.Id = customers.Max(r => r.Id) + 1;
            newCustomer.Audit = new Audit("seed", DateTime.Now, DateTime.Now);
            return newCustomer;
        }

        public int Commit()
        {
            return 0;
        }

        public int Count()
        {
            return customers.Count;
        }

        public Customer Delete(int id)
        {
            var customer = customers.FirstOrDefault(r => r.Id == id);
            if (customer != null)
            {
                customers.Remove(customer);
            }
            return customer;
        }

        public Customer GetById(int id)
        {
            return customers.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Customer> GetCustomersByName(string name = null)
        {
            return customers
                .Where(r => string.IsNullOrEmpty(name) || r.FirstName.StartsWith(name) || r.LastName.StartsWith(name))
                .OrderBy(r => r.LastName)
                .Select(r => r);
        }

        public Customer Update(Customer updatedCustomer)
        {
            var customer = customers.SingleOrDefault(r => r.Id == updatedCustomer.Id);
            if (customer != null)
            {
                customer.FirstName = updatedCustomer.FirstName;
                customer.LastName = updatedCustomer.LastName;
                customer.Audit.Updated = DateTime.Now;
            }
            return customer;
        }
    }
}
