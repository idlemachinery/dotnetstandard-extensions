using System;
using System.Linq.Expressions;

namespace IdleMachinery.Extensions.Mapping
{
    /// <summary>
    /// Maps the value of properties on a source object to the value of properties on a target object
    /// </summary>
    /// <typeparam name="TSource">The type of the source object</typeparam>
    /// <typeparam name="TTarget">The type of the target object</typeparam>
    public class Map<TSource, TTarget>
        where TTarget : class, new()
    {
        public TSource Source { get; set; }
        public TTarget Target { get; set; }

        // TODO - document
        public Map(TSource source)
        {
            Source = source;
            Target = new TTarget();
        }

        // TODO - document
        public T GetValue<T>(string propertyName)
        {
            return default(T);
        }

        // TODO - document
        public T GetValue<T>(Expression<Func<TSource, T>> targetPropertyExpression)
        {
            return default(T);
        }

        // TODO - document
        public Map<TSource, TTarget> Populate(string targetPropertyName, string sourcePropertyName)
        {
            var targetAccessor = typeof(TTarget).GetProperty(targetPropertyName);
            var sourceAccessor = typeof(TSource).GetProperty(sourcePropertyName);
            targetAccessor.SetValue(Target, sourceAccessor.GetValue(Source));
            return this;
        }

        // TODO - document
        public Map<TSource, TTarget> Populate<T>(string targetPropertyName, Func<TSource, T> sourceValue)
        {
            var targetAccessor = typeof(TTarget).GetProperty(targetPropertyName);
            targetAccessor.SetValue(Target, sourceValue(Source));
            return this;
        }

        // TODO - document
        public Map<TSource, TTarget> Populate<T>(Expression<Func<TTarget, T>> targetAccessor, Func<TSource, T> sourceValue)
        {
            var targetPropertyInfo = targetAccessor.ToPropertyInfo();
            targetPropertyInfo.SetValue(Target, sourceValue(Source));
            return this;
        }
    }
}
