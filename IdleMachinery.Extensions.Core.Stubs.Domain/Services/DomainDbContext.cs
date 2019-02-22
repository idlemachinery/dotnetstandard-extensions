using IdleMachinery.Extensions.Standard.Stubs.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IdleMachinery.Extensions.Core.Stubs.Domain
{
    public class DomainDbContext : DbContext
    {
        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        //        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=IdleMachinery.Extensions.Core.Domain;Integrated Security=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {        
            modelBuilder.Entity<Customer>(table =>
            {
                table.OwnsOne(x => x.Audit, audit =>
                {
                    audit.Property(x => x.User).HasColumnName("Audit_User");
                    audit.Property(x => x.Created).HasColumnName("Audit_Created");
                    audit.Property(x => x.Updated).HasColumnName("Audit_Updated");
                });
            });

            modelBuilder.Entity<Order>(table =>
            {
                table.OwnsOne(x => x.Audit, audit =>
                {
                    audit.Property(x => x.User).HasColumnName("Audit_User");
                    audit.Property(x => x.Created).HasColumnName("Audit_Created");
                    audit.Property(x => x.Updated).HasColumnName("Audit_Updated");
                });
            });

            modelBuilder.Entity<OrderProduct>()
                .HasKey(t => new { t.OrderId, t.ProductId });

            // cascade if any entity is deleted
            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderProducts)
                .WithOne(x => x.Order)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(x => x.OrderProducts)
                .WithOne(x => x.Product)
                .OnDelete(DeleteBehavior.Cascade);

            //SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var product1 = new Product
            {
                Id = 1,
                Name = "Item 1"
            };
            var product2 = new Product
            {
                Id = 2,
                Name = "Item 2"
            };
            modelBuilder.Entity<Product>().HasData(product1, product2);

            var customer = new Customer
            {
                Id = 1,
                FirstName = "Elton",
                LastName = "Stoneman",
                Audit = new Audit("Seed", DateTime.Now, DateTime.Now)
            };
            modelBuilder.Entity<Customer>().HasData(customer);

            var order = new Order
            {
                Id = 1,
                Reference = Guid.NewGuid().ToString(),
                Customer = customer,
                Audit = new Audit("Seed", DateTime.Now, DateTime.Now)
            };
            order.OrderProducts.Add(new OrderProduct() { Product = product1 });
            order.OrderProducts.Add(new OrderProduct() { Product = product2 });
            modelBuilder.Entity<Order>().HasData(order);
        }

        // https://www.bricelam.net/2016/12/13/validation-in-efcore.html
        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(
                    entity,
                    validationContext,
                    validateAllProperties: true);
            }

            return base.SaveChanges();
        }        
    }
}
