using MailKit.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons
{
    public static class LinQUtils
    {



        public static IQueryable<TEntity> DynamicFilter<TEntity>(this IQueryable<TEntity> source, TEntity entity)
        {
            var properties = entity.GetType().GetProperties();

            foreach (var item in properties)
            {
                if (entity.GetType().GetProperty(item.Name) == null) continue;

                var propertyVal = entity.GetType().GetProperty(item.Name).GetValue(entity, null);
                if (propertyVal == null) continue;
                if (item.CustomAttributes.Any(a => a.AttributeType == typeof(SkipAttribute))) continue;
                bool isDateTime = typeof(DateTime).IsAssignableFrom(item.PropertyType) || typeof(DateTime?).IsAssignableFrom(item.PropertyType);
                if (isDateTime)
                {
                    DateTime dt = (DateTime)propertyVal;
                    source = source.Where($"{item.Name} >= @0 && {item.Name} < @1", dt.Date, dt.Date.AddDays(1));
                }
                else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(ContainAttribute)))
                {
                    var array = (IList)propertyVal;
                    source = source.Where($"{item.Name}.Any(a=> @0.Contains(a))", array);

                }
                else if (item.CustomAttributes.Any(a => a.AttributeType == typeof(SortAttribute)))
                {
                    string[] sort = propertyVal.ToString().Split(", ");
                    if (sort.Length == 2)
                    {
                        if (sort[1].Equals("asc"))
                        {
                            source = source.OrderBy(sort[0]);
                        }

                        if (sort[1].Equals("desc"))
                        {
                            source = source.OrderBy(sort[0] + " descending");
                        }
                    }
                    else
                    {
                        source = source.OrderBy(sort[0]);
                    }
                }
                else if (item.PropertyType == typeof(string))
                {
                    source = source.Where($"{item.Name}.ToLower().Contains(@0)", ((string)propertyVal).Trim().ToLower());
                }
                else
                {
                    source = source.Where($"{item.Name} = \"{propertyVal}\"");
                }
            }
            return source;
        }

        public static (int, IQueryable<TResult>) PagingIQueryable<TResult>(this IQueryable<TResult> source, int page, int size,
            int limitPaging, int defaultPaging)
        {
            if (size > limitPaging)
            {
                size = limitPaging;
            }
            if (size < 1)
            {
                size = defaultPaging;
            }
            if (page < 1)
            {
                page = 1;
            };

            int total = source == null ? 0 : source.Count();

            IQueryable<TResult> results = source
                .Skip((page - 1) * size)
                .Take(size);

            return (total, results);
        }

        public static IQueryable<TEntity> DynamicFilterForAdmin<TEntity>(this IQueryable<TEntity> source, string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                source = source.Where(BuildDynamicFilter<TEntity>(search));
            }

            return source;
        }

        public static Expression<Func<T, bool>> BuildDynamicFilter<T>(string searchTerm)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            Expression orExpression = null;

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    var propertyExpression = Expression.Property(parameter, property);
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var searchExpression = Expression.Constant(searchTerm, typeof(string));

                    var containsExpression = Expression.Call(propertyExpression, containsMethod, searchExpression);


                    if(orExpression == null)
                    {
                        orExpression = containsExpression;
                    }
                    else
                    {
                        orExpression = Expression.OrElse(orExpression, containsExpression);

                    }

                }
            }

            // Nếu không có thuộc tính nào để lọc, trả về biểu thức "true" (để không lọc gì cả).
            if (orExpression == null)
            {
                var trueConstant = Expression.Constant(true);
                orExpression = trueConstant;
            }

            return Expression.Lambda<Func<T, bool>>(orExpression, parameter);
        }
    }
}
