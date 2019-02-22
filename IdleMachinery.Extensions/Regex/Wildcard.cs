namespace System.Text.RegularExpressions
{
    /// <summary>
    /// Represents a wildcard running on the
    /// <see cref="System.Text.RegularExpressions"/> engine.
    /// </summary>
    public class Wildcard : Regex
    {       
        /// <summary>
        /// Initializes a wildcard with the given search pattern.
        /// </summary>
        /// <param name="pattern">The wildcard pattern to match.</param>
        /// <param name="includeBeginLine">Include the beginning of line or string character.</param>
        /// <param name="includeEndLine">Include the end of line or string character.</param>
        public Wildcard(string pattern, 
            bool includeBeginLine = true, bool includeEndLine = true)
       : base(WildcardToRegex(pattern, includeBeginLine, includeEndLine))
        {            
        }

        /// <summary>
        /// Initializes a wildcard with the given search pattern and options.
        /// </summary>
        /// <param name="pattern">The wildcard pattern to match.</param>
        /// <param name="options">A combination of one or more
        /// <see cref="System.Text.RegexOptions"/>.</param>
        /// <param name="includeBeginLine">Include the beginning of line or string character.</param>
        /// <param name="includeEndLine">Include the end of line or string character.</param>
        public Wildcard(string pattern, RegexOptions options,
            bool includeBeginLine = true, bool includeEndLine = true)
       : base(WildcardToRegex(pattern, includeBeginLine, includeEndLine), options)
        {           
        }

        /// <summary>
        /// Converts a wildcard to a regex.
        /// </summary>
        /// <param name="pattern">The wildcard pattern to convert.</param>
        /// <param name="includeBeginLine">Include the beginning of line or string character.</param>
        /// <param name="includeEndLine">Include the end of line or string character.</param>
        /// <returns>A regex equivalent of the given wildcard.</returns>
        public static string WildcardToRegex(string pattern,
            bool includeBeginLine = true, bool includeEndLine = true)
        {
            var ret = includeBeginLine ? "^" : "";
            ret += Regex.Escape(pattern)
                    .Replace("\\*", ".*")
                    .Replace("\\?", ".");
            ret += includeEndLine ? "$" : "";
            return ret;
        }
    }
}
