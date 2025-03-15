using System.Linq.Expressions;

namespace ETechParking.Infrastructure.Data.Shared.Filters;

public static class DateTimeFilterHelper
{
    public static Expression<Func<TEntity, bool>> CreateDateFilter<TEntity>(
        Expression<Func<TEntity, DateTime>> propertySelector,
        DateTime? fromDate,
        DateTime? toDate)
    {
        if (!fromDate.HasValue && !toDate.HasValue)
            return entity => true;

        var parameter = propertySelector.Parameters[0];
        var propertyAccess = propertySelector.Body;

        Expression condition = default!;

        if (fromDate.HasValue)
        {
            var fromDateValue = fromDate.Value.Date;
            var fromCondition = Expression.GreaterThanOrEqual(propertyAccess, Expression.Constant(fromDateValue));
            condition = condition == null ? fromCondition : Expression.AndAlso(condition, fromCondition);
        }

        if (toDate.HasValue)
        {
            var toDateValue = toDate.Value.Date.AddDays(1);
            var toCondition = Expression.LessThan(propertyAccess, Expression.Constant(toDateValue));
            condition = condition == null ? toCondition : Expression.AndAlso(condition, toCondition);
        }

        return Expression.Lambda<Func<TEntity, bool>>(condition!, parameter);
    }

    public static Expression<Func<TEntity, bool>> CreateExactDateFilter<TEntity>(
        Expression<Func<TEntity, DateTime>> propertySelector,
        DateTime exactDate)
    {
        var parameter = propertySelector.Parameters[0];
        var propertyAccess = propertySelector.Body;
        var startOfDay = exactDate.Date;
        var endOfDay = startOfDay.AddDays(1).AddTicks(-1);
        var condition = Expression.AndAlso(
            Expression.GreaterThanOrEqual(propertyAccess, Expression.Constant(startOfDay)),
            Expression.LessThanOrEqual(propertyAccess, Expression.Constant(endOfDay))
        );

        return Expression.Lambda<Func<TEntity, bool>>(condition, parameter);
    }
}