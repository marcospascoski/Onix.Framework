using Onix.Framework.Infra.Data.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Onix.Framework.Infra.Data.Implementation.Helpers
{
    public static class DataHelper
    {
        public static IQueryable<T> Page<T>(IQueryable<T> source, IPaged paged)
        {
            return OrderBy(source, paged)
                .Skip(paged.SkipItems)
                .Take(paged.PageSize);
        }

        public static IQueryable<T> OrderBy<T>(IQueryable<T> source, IOrdered ordered)
        {
            if (source == null) ArgumentNullException.ThrowIfNull("source");
            if (string.IsNullOrWhiteSpace(ordered?.PropertyName))
            {
                return source;
            }
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            var pi = type.GetProperty(ordered.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (pi != null)
            {
                Expression expr = Expression.Property(arg, pi);
                type = pi.PropertyType;
                var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
                var lambda = Expression.Lambda(delegateType, expr, arg);
                string methodName = ordered.IsDescending ? "OrderByDescending" : "OrderBy";
                object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
                return (IQueryable<T>)result;
            }
            return source;
        }
    }
}