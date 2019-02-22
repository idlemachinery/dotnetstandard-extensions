using IdleMachinery.Extensions.Standard.Stubs.Domain;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace IdleMachinery.Extensions.Framework.Stubs.Domain
{
    public static class DbContextExtensions
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
                    audited.Audit.User = user ?? new WindowsPrincipal(WindowsIdentity.GetCurrent()).Identity.Name;
                }
            }
        }
    }
}
