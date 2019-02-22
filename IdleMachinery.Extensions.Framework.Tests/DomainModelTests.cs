using System;
using System.Linq;
using IdleMachinery.Extensions.Framework.Stubs.Domain;
using IdleMachinery.Extensions.Standard.Stubs.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdleMachinery.Extensions.Framework.Tests
{
    [TestClass]
    public class DomainModelTests
    {
        [TestMethod]
        //[Ignore("Initial integration test. Run once.")]
        public void SaveChanges()
        {
            using (var db = new DomainDbContext())
            {
                var products = db.Products.ToList();
                var customer = db.Customers.FirstOrDefault();
                if (customer == null)
                {
                    customer = new Customer
                    {
                        FirstName = "Elton",
                        LastName = "Stoneman"
                    };
                }    
                
                var order = new Order
                {
                    Reference = Guid.NewGuid().ToString(),
                    Customer = customer
                };
                foreach (var product in products)
                {
                    order.OrderProducts.Add(new OrderProduct() { Product = product });
                }
                db.Orders.Add(order);
                db.Save();
            }
        }

        [TestMethod]
        public void UpdateOrder()
        {
            using (var db = new DomainDbContext())
            {
                var order = db.Orders.FirstOrDefault();
                if (order != null)
                {
                    order.Reference += ".1";
                    db.Save();
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateProduct()
        {
            using (var db = new DomainDbContext())
            {
                var product = db.Products.FirstOrDefault();
                if (product != null)
                {
                    product.Name += " new";
                    db.Save();
                }
            }
        }
    }
}
