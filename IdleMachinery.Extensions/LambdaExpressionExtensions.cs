using System.Reflection;

namespace System.Linq.Expressions
{
    public static class LambdaExpressionExtensions
    {

        // Reflection with strings
        // - Access members by name
        // - No compile-time checking
        // - When definitions change, code breaks at runtime

        /// <summary>
        /// Allows an objects property to be specified through a lambda expression to retrieve it's PropertyInfo.
        /// </summary>
        /// <param name="expression">The expression that specifies a property</param>
        /// <returns>The PropertyInfo for apecified property</returns>
        public static PropertyInfo ToPropertyInfo(this LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            return memberExpression.Member as PropertyInfo; // should be checked to make sure the accessor is a property
        }
    }
}
