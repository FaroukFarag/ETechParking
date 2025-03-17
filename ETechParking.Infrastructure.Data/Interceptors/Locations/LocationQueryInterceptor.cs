using ETechParking.Domain.Interfaces.Services.Locations.Users;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Text.RegularExpressions;

public class LocationFilterInterceptor(IUserContextService userContextService) : IDbCommandInterceptor
{
    private readonly IUserContextService _userContextService = userContextService;

    public InterceptionResult<DbCommand> CommandCreating(
        CommandCorrelatedEventData eventData,
        InterceptionResult<DbCommand> result)
    {
        if (!_userContextService.IsAdmin() && _userContextService.IsAuthenticated())
        {
            var locationId = _userContextService.GetLocationId();
            if (locationId.HasValue)
            {
                // Check if the result has a valid DbCommand
                if (result.HasResult)
                {
                    var command = result.Result;
                    command.CommandText = ApplyLocationFilter(command.CommandText, locationId.Value);

                    // Add the LocationId parameter to avoid SQL injection
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@LocationId";
                    parameter.Value = locationId.Value;
                    command.Parameters.Add(parameter);
                }
                else
                {
                    // If there is no result, create a new DbCommand and set its CommandText
                    var command = eventData.Connection.CreateCommand();
                    command.CommandText = ApplyLocationFilter("SELECT 1", locationId.Value); // Example query
                    result = InterceptionResult<DbCommand>.SuppressWithResult(command);
                }
            }
        }

        return result;
    }

    private static string ApplyLocationFilter(string commandText, int locationId)
    {
        // Regex to identify the WHERE clause (case-insensitive)
        var whereRegex = new Regex(@"\bWHERE\b", RegexOptions.IgnoreCase);

        // Regex to identify ORDER BY or GROUP BY clauses (case-insensitive)
        var orderOrGroupByRegex = new Regex(@"\b(ORDER BY|GROUP BY)\b", RegexOptions.IgnoreCase);

        // Check if the query already has a WHERE clause
        if (whereRegex.IsMatch(commandText))
        {
            // Insert the LocationId filter before the existing WHERE clause
            var whereMatch = whereRegex.Match(commandText);
            var beforeWhere = commandText.Substring(0, whereMatch.Index);
            var afterWhere = commandText.Substring(whereMatch.Index);

            return $"{beforeWhere} AND LocationId = @LocationId {afterWhere}";
        }
        // Check if the query has ORDER BY or GROUP BY clauses
        else if (orderOrGroupByRegex.IsMatch(commandText))
        {
            // Insert the WHERE clause before ORDER BY or GROUP BY
            var orderOrGroupByMatch = orderOrGroupByRegex.Match(commandText);
            var beforeOrderOrGroupBy = commandText.Substring(0, orderOrGroupByMatch.Index);
            var afterOrderOrGroupBy = commandText.Substring(orderOrGroupByMatch.Index);

            return $"{beforeOrderOrGroupBy} WHERE LocationId = @LocationId {afterOrderOrGroupBy}";
        }
        else
        {
            // If there is no WHERE, ORDER BY, or GROUP BY, append the WHERE clause at the end
            return $"{commandText} WHERE LocationId = @LocationId";
        }
    }
}