using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Entity
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
            catch (DbEntityValidationException ex)
            {
                // Add the original exception as the innerException
                throw new DbEntityValidationException(ex.BuildExceptionMessage(), ex);
            }
        }

        // TODO - document
        public static async Task<int> SaveChangesWithErrorsAsync(this DbContext db)
        {
            try
            {
                return await db.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                // Add the original exception as the innerException
                throw new DbEntityValidationException(ex.BuildExceptionMessage(), ex);
            }
        }

        private static string BuildExceptionMessage(this DbEntityValidationException ex)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var failure in ex.EntityValidationErrors)
            {
                sb.AppendFormat($"{failure.Entry.Entity.GetType()} failed validation\n");
                foreach (var error in failure.ValidationErrors)
                {
                    sb.AppendFormat($"- {error.PropertyName} : {error.ErrorMessage}");
                    sb.AppendLine();
                }
            }
            return "Entity Validation Failed - errors follow:\n" + sb.ToString();
        }

        // TODO - document
        public static IEnumerable<T> ExecuteQuery<T>(this DbContext db, string query)
        {
            return db.Database.SqlQuery<T>(query).ToList();
        }

        // TODO - document
        public static async Task<IEnumerable<T>> ExecuteQueryAsync<T>(this DbContext db, string query)
        {
            return await db.Database.SqlQuery<T>(query).ToListAsync();
        }

        /// <summary>
        /// Execute the passed Stored Procedure.
        /// </summary>
        /// <typeparam name="T">The return Entity(Object) type</typeparam>
        /// <param name="SPname">The stored procedure name</param>
        /// <param name="parameters">The parameters IN ORDER</param>
        /// <returns>A List of type T</returns>
        public static IEnumerable<T> ExecuteStoreQuery<T>(this DbContext db, string SPname, params object[] parameters)
        {
            int i = 0;
            string para = string.Join(", ", (from p in parameters
                                             select string.Concat(new object[] { "@p", i++ }))
                                             .ToList());

            return db.Database.SqlQuery<T>(SPname + " " + para, parameters).ToList();
        }

        // TODO - document
        public static void ExecuteTSql(this DbContext db, string query)
        {
            query = query.Replace("GO", ";");
            var scripts = query.Split(';').ToList();
            foreach (var script in scripts)
            {
                if (!string.IsNullOrEmpty(script))
                    db.Database.ExecuteSqlCommand(script);
            }
        }
    }
}
