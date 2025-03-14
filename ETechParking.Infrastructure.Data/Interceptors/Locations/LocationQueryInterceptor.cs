using ETechParking.Domain.Interfaces.Services.Locations.Users;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace ETechParking.Infrastructure.Data.Interceptors;



public class LocationFilterInterceptor(IUserContextService userContextService) : DbCommandInterceptor
{
    private readonly IUserContextService _userContextService = userContextService;

    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        ModifyCommand(command);
        return base.ReaderExecuting(command, eventData, result);
    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default)
    {
        ModifyCommand(command);
        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }

    private void ModifyCommand(DbCommand command)
    {
        if (!_userContextService.IsAdmin() && _userContextService.IsAuthenticated())
        {
            var locationId = _userContextService.GetLocationId();

            if (locationId.HasValue)
            {
                var originalSql = command.CommandText;
                var newSql = $"{originalSql} WHERE LocationId = @LocationId";

                command.CommandText = newSql;

                var parameter = command.CreateParameter();

                parameter.ParameterName = "@LocationId";
                parameter.Value = locationId.Value;
                command.Parameters.Add(parameter);
            }
        }
    }
}
