using System.Linq.Expressions;

namespace SU24SE069_PLATFORM_KAROKE_Service.Helpers
{
    /// <summary>
    /// A utility class for combining multiple LINQ expressions into a single expression.
    /// </summary>
    public static class ExpressionCombiner
    {
        /// <summary>
        /// Combines multiple expressions using the logical AND operator.
        /// </summary>
        /// <typeparam name="T">The type of the parameter for the expressions.</typeparam>
        /// <param name="expressions">An array of expressions to combine.</param>
        /// <returns>A single expression representing the logical AND of the provided expressions.</returns>
        /// <exception cref="ArgumentException">Thrown if no expressions are provided.</exception>
        public static Expression<Func<T, bool>> CombineExpressionsWithAnd<T>(params Expression<Func<T, bool>>?[] expressions)
        {
            if (expressions == null || expressions.Length == 0)
                throw new ArgumentException("No expressions provided");

            // If all expressions are null, return a expression with no filter conditions
            if (expressions.All(exp => exp == null))
            {
                return s => true;
            }

            // Start with a default expression that is always true
            Expression<Func<T, bool>> result = s => true;

            foreach (var expression in expressions)
            {
                if (expression != null)
                {
                    result = CombineTwoExpressionsWithAnd(result, expression);
                }
            }

            return result;
        }

        /// <summary>
        /// Combines multiple expressions using the logical OR operator.
        /// </summary>
        /// <typeparam name="T">The type of the parameter for the expressions.</typeparam>
        /// <param name="expressions">An array of expressions to combine.</param>
        /// <returns>A single expression representing the logical OR of the provided expressions.</returns>
        public static Expression<Func<T, bool>> CombineExpressionsWithOr<T>(params Expression<Func<T, bool>>?[] expressions)
        {
            if (expressions == null || expressions.Length == 0)
                throw new ArgumentException("No expressions provided");

            // If all expressions are null, return a expression with no filter conditions
            if (expressions.All(exp => exp == null))
            {
                return s => true;
            }

            // Start with a default expression that is always false
            Expression<Func<T, bool>> result = s => false;

            foreach (var expression in expressions)
            {
                if (expression != null)
                {
                    result = CombineTwoExpressionsWithOr(result, expression);
                }
            }

            return result;
        }

        /// <summary>
        /// Combines two expressions using the logical AND operator.
        /// </summary>
        /// <typeparam name="T">The type of the parameter for the expressions.</typeparam>
        /// <param name="first">The first expression.</param>
        /// <param name="second">The second expression.</param>
        /// <returns>A single expression representing the logical AND of the provided expressions.</returns>
        private static Expression<Func<T, bool>> CombineTwoExpressionsWithAnd<T>(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var firstBody = Expression.Invoke(first, parameter);
            var secondBody = Expression.Invoke(second, parameter);

            var combinedBody = Expression.AndAlso(firstBody, secondBody);

            return Expression.Lambda<Func<T, bool>>(combinedBody, parameter);
        }

        /// <summary>
        /// Combines two expressions using the logical OR operator.
        /// </summary>
        /// <typeparam name="T">The type of the parameter for the expressions.</typeparam>
        /// <param name="first">The first expression.</param>
        /// <param name="second">The second expression.</param>
        /// <returns>A single expression representing the logical OR of the provided expressions.</returns>
        private static Expression<Func<T, bool>> CombineTwoExpressionsWithOr<T>(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var firstBody = Expression.Invoke(first, parameter);
            var secondBody = Expression.Invoke(second, parameter);

            var combinedBody = Expression.OrElse(firstBody, secondBody);

            return Expression.Lambda<Func<T, bool>>(combinedBody, parameter);
        }
    }
}
