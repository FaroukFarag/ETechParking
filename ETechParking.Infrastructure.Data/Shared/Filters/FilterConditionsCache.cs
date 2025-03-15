using System.Linq.Expressions;

namespace ETechParking.Infrastructure.Data.Shared.Filters;

public static class FilterConditionsCache
{
    private static readonly Dictionary<Type, Dictionary<string, Func<Expression, Expression, Expression>>> Cache = new();

    public static Dictionary<string, Func<Expression, Expression, Expression>> GetFilterConditions<TFilterDto>()
    {
        var type = typeof(TFilterDto);

        if (Cache.TryGetValue(type, out var cachedConditions))
            return cachedConditions;

        var conditions = type.GetProperties()
            .Where(p => p.Name.StartsWith("From") || p.Name.StartsWith("To"))
            .ToDictionary(
                p => p.Name,
                p => p.Name.StartsWith("From")
                    ? (Func<Expression, Expression, Expression>)((left, right) => Expression.GreaterThanOrEqual(left, right))
                    : (left, right) => Expression.LessThanOrEqual(left, right)
            );

        Cache[type] = conditions;

        return conditions;
    }
}