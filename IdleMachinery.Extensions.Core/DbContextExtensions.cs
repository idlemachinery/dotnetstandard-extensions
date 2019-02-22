using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace IdleMachinery.Extensions.Core
{
    public static class DbContextExtensions
    {
        // TODO - document
        public static int SaveChangesWithErrors(this DbContext db)
        {
            try
            {
                return db.SaveChanges();
            }
            catch (ValidationException ex)
            {
                // Add the original exception as the innerException
                throw new ValidationException(ex.BuildExceptionMessage(), ex);
            }
        }

        // TODO - document
        public static async Task<int> SaveChangesWithErrorsAsync(this DbContext db)
        {
            try
            {
                return await db.SaveChangesAsync();
            }
            catch (ValidationException ex)
            {
                // Add the original exception as the innerException
                throw new ValidationException(ex.BuildExceptionMessage(), ex);
            }
        }

        private static string BuildExceptionMessage(this ValidationException ex)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var failure in ex.Data.Keys)
            {
                sb.AppendFormat($"{failure} failed validation\n");
                sb.AppendFormat($"- {ex.Data[failure]}");
                sb.AppendLine();
            }
            return "Entity Validation Failed - errors follow:\n" + sb.ToString();
        }
    }
}
