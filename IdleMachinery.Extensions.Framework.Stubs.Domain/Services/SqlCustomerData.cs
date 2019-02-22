using IdleMachinery.Extensions.Standard.Stubs.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IdleMachinery.Extensions.Framework.Stubs.Domain
{
    public class SqlCustomerData : ICustomerData
    {
        private readonly DomainDbContext db;

        public SqlCustomerData(DomainDbContext db)
        {
            this.db = db;
        }

        public Customer Add(Customer newCustomer)
        {
            db.Customers.Add(newCustomer);
            return newCustomer;
        }

        public int Commit()
        {
            //return db.SaveChanges();
            return db.Save();
        }

        public int Count()
        {
            return db.Customers.Count();
        }

        public Customer Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
            }
            return customer;
        }

        public Customer GetById(int id)
        {
            return db.Customers.Find(id);
        }

        public IEnumerable<Customer> GetCustomersByName(string name = null)
        {
            var query = from r in db.Customers
                        where string.IsNullOrEmpty(name) || r.FirstName.StartsWith(name) || r.LastName.StartsWith(name)
                        orderby r.LastName
                        select r;
            return query;
        }

        public Customer Update(Customer updatedCustomer)
        {
            var entity = db.Customers.Attach(updatedCustomer);
            db.Entry(entity).State = EntityState.Modified;
            return updatedCustomer;
        }
    }
}
