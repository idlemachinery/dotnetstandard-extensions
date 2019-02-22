using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Retrieves and concatenates the value of the exception message,
        /// including all inner exceptions.
        /// </summary>
        /// <param name="ex">The thrown exception.</param>
        /// <returns>A string of exception messages joined with a newline.</returns>
        public static string FullMessage(this Exception ex)
        {
            var builder = new StringBuilder();
            while (ex != null)
            {
                builder.Append($"{ex.Message}{Environment.NewLine}");
                ex = ex.InnerException;
            }
            return builder.ToString();
        }

        /// <summary>
        /// Indicates whether the excaption has an InnerException.
        /// </summary>
        /// <param name="ex">An Exception.</param>
        /// <returns>A boolean indication of whether or not there is an InnerException.</returns>
        public static bool HasInnerException(this Exception ex)
        {
            return ex.InnerException != null;
        }

        /// <summary>
        /// Retrieves all associated exceptions starting with the outermost and moving in.
        /// </summary>
        /// <param name="ex">An Exception.</param>
        /// <returns>An IEnumerable list of Exceptions.</returns>
        public static IEnumerable<Exception> GetExceptionStack(this Exception ex)
        {
            var exceptions = new List<Exception>();
            var nextEx = ex;
            while (nextEx != null)
            {
                exceptions.Add(nextEx);
                nextEx = nextEx.InnerException;
            }
            return exceptions;
        }
    }
}
