using IdleMachinery.Extensions.Standard.Stubs.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace IdleMachinery.Extensions.Core.Stubs.Domain
{
    public static class DomainDbContextExtensions
    {
        public static int Save(this DomainDbContext context, string user = null)
        {
            context.Audit(user);
            return context.SaveChangesWithErrors();
        }

        public static async Task<int> SaveAsync(this DomainDbContext context, string user = null)
        {
            context.Audit(user);
            return await context.SaveChangesWithErrorsAsync();
        }

        private static void Audit(this DomainDbContext context, string user = null)
        {
            foreach (var change in context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                var readOnly = change.Entity as IReadOnly;
                if (readOnly != null)
                {
                    //change.State = EntityState.Unchanged;
                    //continue;
                    throw new InvalidOperationException("Attempt to change read-only data");
                }
                var audited = change.Entity as IAudited;
                if (audited != null)
                {
                    if (change.State == EntityState.Added)
                    {
                        audited.Audit.Created = DateTime.Now;
                    }
                    audited.Audit.Updated = DateTime.Now;
                    audited.Audit.User = user ?? WindowsIdentity.GetCurrent().Name;
                }
            }
        }

        public static void EnsureSeedDataForContext(this DomainDbContext context)
        {
            // first, clear the database.  This ensures we can always start 
            // fresh with each demo.  Not advised for production environments, obviously :-)

            context.Orders.RemoveRange(context.Orders);
            context.Products.RemoveRange(context.Products);
            context.Customers.RemoveRange(context.Customers);

            context.SaveChanges();

            // init seed data
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Item 1"
                },
                new Product
                {
                    Name = "Item 2"
                }
            };

            var customers = new List<Customer>()
            {
                new Customer
                {
                    FirstName = "Elton",
                    LastName = "Stoneman",
                    Audit = new Audit("seed", DateTime.Now, DateTime.Now)
                },
                new Customer
                {
                    FirstName = "Bill",
                    LastName = "Sprigs",
                    Audit = new Audit("seed", DateTime.Now, DateTime.Now)
                },
                new Customer
                {
                    FirstName = "Susie",
                    LastName = "Franklin",
                    Audit = new Audit("seed", DateTime.Now, DateTime.Now)
                }
            };

            var orders = new List<Order>()
            {
                new Order
                {
                    Reference = Guid.NewGuid().ToString(),
                    Customer = customers[0],
                    OrderProducts = new List<OrderProduct>
                    {
                        new OrderProduct() { Product = products[0] },
                        new OrderProduct() { Product = products[1] }

                    },
                    Audit = new Audit("seed", DateTime.Now, DateTime.Now)
                },
                new Order
                {
                    Reference = Guid.NewGuid().ToString(),
                    Customer = customers[1],
                    OrderProducts = new List<OrderProduct>
                    {
                        new OrderProduct() { Product = products[0] },
                        new OrderProduct() { Product = products[1] }

                    },
                    Audit = new Audit("seed", DateTime.Now, DateTime.Now)
                },
                new Order
                {
                    Reference = Guid.NewGuid().ToString(),
                    Customer = customers[2],
                    OrderProducts = new List<OrderProduct>
                    {
                        new OrderProduct() { Product = products[0] },
                        new OrderProduct() { Product = products[1] }

                    },
                    Audit = new Audit("seed", DateTime.Now, DateTime.Now)
                }
            };

            context.Products.AddRange(products);
            context.Customers.AddRange(customers);
            context.Orders.AddRange(orders);

            context.SaveChanges();
        }
    }
}
