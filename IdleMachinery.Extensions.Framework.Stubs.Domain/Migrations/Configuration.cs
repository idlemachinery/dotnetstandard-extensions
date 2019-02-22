namespace IdleMachinery.Extensions.Framework.Stubs.Domain.Migrations
{
    using IdleMachinery.Extensions.Framework.Stubs.Domain;
    using IdleMachinery.Extensions.Standard.Stubs.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DomainDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DomainDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var product1 = new Product
            {
                Name = "Item 1"
            };
            var product2 = new Product
            {
                Name = "Item 2"
            };
            context.Products.AddOrUpdate(x => x.Name, product1, product2);
            context.SaveChanges();

            var customer = new Customer
            {
                FirstName = "Elton",
                LastName = "Stoneman"
            };
            context.Customers.AddOrUpdate(x => x.LastName, customer);                       

            if (!context.Orders.Any())
            {                
                var order = new Order
                {
                    Reference = Guid.NewGuid().ToString(),
                    Customer = customer
                };
                order.OrderProducts.Add(new OrderProduct() { Product = product1 });
                order.OrderProducts.Add(new OrderProduct() { Product = product2 });
                context.Orders.AddOrUpdate(order);
            }
            context.Save();
            //context.SaveChanges();
        }
    }
}
