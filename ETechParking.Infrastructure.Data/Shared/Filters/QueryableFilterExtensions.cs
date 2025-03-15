using ETechParking.Common.Utilities;
using ETechParking.Domain.Shared.Attributs;
using System.Linq.Expressions;
using System.Reflection;

namespace ETechParking.Infrastructure.Data.Shared.Filters;

public static class QueryableFilterExtensions
{
    public static Expression<Func<TEntity, bool>> ToPredicate<TEntity, TFilterDto>(this TFilterDto filterDto)
    {
        var predicate = PredicateBuilder.True<TEntity>();
        var filterConditions = FilterConditionsCache.GetFilterConditions<TFilterDto>();

        foreach (var filterProperty in GetNonNullProperties(filterDto))
        {
            var entityPropertyName = filterProperty.GetCustomAttribute<FilterPropertyAttribute>()?.EntityPropertyName ?? filterProperty.Name;

            if (typeof(TEntity).GetProperty(entityPropertyName) is not { } entityProperty)
                continue;

            var parameter = Expression.Parameter(typeof(TEntity), "t");
            var propertyAccess = Expression.Property(parameter, entityProperty);
            var filterValue = filterProperty.GetValue(filterDto);

            Expression condition;

            if (entityProperty.PropertyType == typeof(DateTime) || entityProperty.PropertyType == typeof(DateTime?))
            {
                var dateTimeValue = (DateTime?)filterValue;

                if (dateTimeValue.HasValue)
                {
                    // Unwrap the nullable DateTime property if necessary
                    var unwrappedPropertyAccess = entityProperty.PropertyType == typeof(DateTime?)
                        ? Expression.Property(propertyAccess, "Value") // Access the Value property of Nullable<DateTime>
                        : propertyAccess;

                    if (filterProperty.Name.StartsWith("From", StringComparison.OrdinalIgnoreCase))
                    {
                        var fromDate = dateTimeValue.Value.Date;
                        condition = Expression.GreaterThanOrEqual(unwrappedPropertyAccess, Expression.Constant(fromDate));
                    }
                    else if (filterProperty.Name.StartsWith("To", StringComparison.OrdinalIgnoreCase))
                    {
                        var toDate = dateTimeValue.Value.Date.AddDays(1);
                        condition = Expression.LessThan(unwrappedPropertyAccess, Expression.Constant(toDate));
                    }
                    else
                    {
                        var exactDate = dateTimeValue.Value.Date;
                        condition = DateTimeFilterHelper.CreateExactDateFilter(
                            Expression.Lambda<Func<TEntity, DateTime>>(unwrappedPropertyAccess, parameter),
                            exactDate
                        ).Body;
                    }
                }
                else
                {
                    // No date filter applied
                    condition = Expression.Constant(true);
                }
            }
            else
            {
                // Handle non-DateTime properties
                var constant = Expression.Constant(filterValue, entityProperty.PropertyType);
                var converted = Expression.Convert(constant, entityProperty.PropertyType);

                condition = filterConditions.TryGetValue(filterProperty.Name, out var filterExpression)
                    ? filterExpression(propertyAccess, converted)
                    : Expression.Equal(propertyAccess, converted);
            }

            predicate = predicate.And(Expression.Lambda<Func<TEntity, bool>>(condition, parameter));
        }

        return predicate;
    }

    private static IEnumerable<PropertyInfo> GetNonNullProperties<TFilterDto>(TFilterDto filterDto) =>
        typeof(TFilterDto).GetProperties().Where(p => p.GetValue(filterDto) != null);
}