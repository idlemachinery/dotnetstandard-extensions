using IdleMachinery.Extensions.Standard.Stubs.Domain;
using System.Data.Entity;

namespace IdleMachinery.Extensions.Framework.Stubs.Domain
{
    public class DomainDbContext : DbContext
    {
        public DomainDbContext() : base("DefaultConnection")
        {
        }
        public DomainDbContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.ComplexType<Audit>();
            modelBuilder.ComplexType<Audit>().Property(x => x.User).HasColumnName("Audit_User");
            modelBuilder.ComplexType<Audit>().Property(x => x.Created).HasColumnName("Audit_Created");
            modelBuilder.ComplexType<Audit>().Property(x => x.Updated).HasColumnName("Audit_Updated");

            modelBuilder.Entity<OrderProduct>()
                .HasKey(t => new { t.OrderId, t.ProductId });

            // cascade if any entity is deleted
            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Orders)
                .WithRequired(x => x.Customer)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderProducts)
                .WithRequired(x => x.Order)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Product>()
                .HasMany(x => x.OrderProducts)
                .WithRequired(x => x.Product)
                .WillCascadeOnDelete();
        }        
    }
}
