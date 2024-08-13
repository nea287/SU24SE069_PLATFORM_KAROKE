using AutoMapper.Execution;
using MailKit.Search;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
                //string x = BuildDynamicFilter<TEntity>(search).ToString();
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
                if (property.PropertyType == typeof(Guid?) || property.PropertyType == typeof(Guid)) continue;
                else if (property.PropertyType == typeof(string))
                {
                    var propertyExpression = Expression.Property(parameter, property); // x.UserName

                    // Kiểm tra null: x.UserName != null
                    var notNullExpression = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(string)));

                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var searchExpression = Expression.Constant(searchTerm, typeof(string)); // "Test Create Account2"

                    // Gọi Contains: x.UserName.Contains("searchTerm")
                    var containsExpression = Expression.Call(propertyExpression, containsMethod, searchExpression);

                    // Kết hợp điều kiện null và Contains: (x.UserName != null) && x.UserName.Contains("searchTerm")
                    var notNullAndContainsExpression = Expression.AndAlso(notNullExpression, containsExpression);

                    if (orExpression == null)
                    {
                        orExpression = notNullAndContainsExpression;
                    }
                    else
                    {
                        orExpression = Expression.OrElse(orExpression, notNullAndContainsExpression);
                    }
                }
                else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                {
                    if (!bool.TryParse(searchTerm, out bool searchValue))
                        continue;

                    var propertyExpression = Expression.Property(parameter, property); // x.SomeBoolProperty

                    // Tạo biểu thức so sánh: x.SomeBoolProperty == searchValue
                    var equalsExpression = Expression.Equal(
                        Expression.Convert(propertyExpression, typeof(bool)),
                        Expression.Constant(searchValue, typeof(bool))
                    );

                    // Nếu là Nullable<bool>, cần kiểm tra null trước khi so sánh
                    Expression notNullAndEqualsExpression = equalsExpression;
                    if (property.PropertyType == typeof(bool?))
                    {
                        var notNullExpression = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(bool?)));
                        notNullAndEqualsExpression = Expression.AndAlso(notNullExpression, equalsExpression);
                    }

                    if (orExpression == null)
                    {
                        orExpression = notNullAndEqualsExpression;
                    }
                    else
                    {
                        orExpression = Expression.OrElse(orExpression, notNullAndEqualsExpression);
                    }
                }
                else if (property.PropertyType == typeof(int) ||
                         property.PropertyType == typeof(float) ||
                         property.PropertyType == typeof(double) ||
                         property.PropertyType == typeof(decimal) ||
                        property.PropertyType == typeof(int?) ||
                         property.PropertyType == typeof(float?) ||
                         property.PropertyType == typeof(double?) ||
                         property.PropertyType == typeof(decimal?))
                {
                    var numericRegex = new Regex(@"^-?\d+(\.\d+)?$");

                    if (!numericRegex.IsMatch(searchTerm))
                    {
                        continue; // Bỏ qua nếu searchTerm không phải là số
                    }

                    // Sử dụng Convert.ChangeType để chuyển đổi searchTerm sang kiểu phù hợp
                    var searchValue = Convert.ChangeType(searchTerm, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);

                    var propertyExpression = Expression.Property(parameter, property); // x.SomeNumericProperty

                    // Tạo biểu thức so sánh: x.SomeNumericProperty == searchValue
                    var equalsExpression = Expression.Equal(
                        Expression.Convert(propertyExpression, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType),
                        Expression.Constant(searchValue)
                    );

                    // Nếu là nullable, cần kiểm tra null trước khi so sánh
                    Expression notNullAndEqualsExpression = equalsExpression;
                    if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                    {
                        var notNullExpression = Expression.NotEqual(propertyExpression, Expression.Constant(null, property.PropertyType));
                        notNullAndEqualsExpression = Expression.AndAlso(notNullExpression, equalsExpression);
                    }

                    if (orExpression == null)
                    {
                        orExpression = notNullAndEqualsExpression;
                    }
                    else
                    {
                        orExpression = Expression.OrElse(orExpression, notNullAndEqualsExpression);
                    }
                }else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    // Giả sử bạn có các biến 'startDate' và 'endDate' là kiểu string và chứa ngày
                    if (!DateTime.TryParse(searchTerm, out DateTime searchDate))
                    {
                        continue; // Bỏ qua nếu searchTerm không phải là ngày hợp lệ
                    }

                    var propertyExpression = Expression.Property(parameter, property); // x.SomeDateTimeProperty

                    // Kiểm tra kiểu Nullable<DateTime>
                    var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    Expression notNullExpression = null;

                    // Tạo biểu thức so sánh: x.SomeDateTimeProperty >= searchDate
                    var greaterThanOrEqualExpression = Expression.GreaterThanOrEqual(
                        Expression.Convert(propertyExpression, propertyType),
                        Expression.Constant(searchDate, propertyType)
                    );

                    // Tạo biểu thức so sánh: x.SomeDateTimeProperty <= searchDate
                    var lessThanOrEqualExpression = Expression.LessThanOrEqual(
                        Expression.Convert(propertyExpression, propertyType),
                        Expression.Constant(searchDate, propertyType)
                    );

                    // Tạo biểu thức lọc lớn hơn hoặc bằng và nhỏ hơn hoặc bằng cho khoảng ngày
                    var dateRangeExpression = Expression.AndAlso(
                        greaterThanOrEqualExpression,
                        lessThanOrEqualExpression
                    );

                    // Nếu là Nullable<DateTime>, cần kiểm tra null trước khi so sánh
                    if (property.PropertyType == typeof(DateTime?))
                    {
                        notNullExpression = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(DateTime?)));
                        dateRangeExpression = Expression.AndAlso(notNullExpression, dateRangeExpression);
                    }

                    if (orExpression == null)
                    {
                        orExpression = dateRangeExpression;
                    }
                    else
                    {
                        orExpression = Expression.OrElse(orExpression, dateRangeExpression);
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

        //public static Expression<Func<T, bool>> BuildDynamicFilter<T>(string searchTerm)
        //{
        //    var parameter = Expression.Parameter(typeof(T), "x");
        //    var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    Expression orExpression = null;

        //    foreach (var property in properties)
        //    {
        //        //var propertyVal = property.GetValue(property, null);
        //        //if (propertyVal == null) continue;
        //        if (property.PropertyType == typeof(string))
        //        {
        //            var propertyExpression = Expression.Property(parameter, property); //$x.UserName
        //            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //            var searchExpression = Expression.Constant(searchTerm, typeof(string)); //"Test Create Account2"

        //            var containsExpression = Expression.Call(propertyExpression, containsMethod, searchExpression); //.Call ($x.UserName).Contains("Test Create Account2")


        //            if (orExpression == null)
        //            {
        //                orExpression = containsExpression;
        //            }
        //            else
        //            {
        //                orExpression = Expression.OrElse(orExpression, containsExpression);

        //            }

        //        }
        //    }

        //    // Nếu không có thuộc tính nào để lọc, trả về biểu thức "true" (để không lọc gì cả).
        //    if (orExpression == null)
        //    {
        //        var trueConstant = Expression.Constant(true);
        //        orExpression = trueConstant;
        //    }



        //    return Expression.Lambda<Func<T, bool>>(orExpression, parameter);
        //}
    }
}
