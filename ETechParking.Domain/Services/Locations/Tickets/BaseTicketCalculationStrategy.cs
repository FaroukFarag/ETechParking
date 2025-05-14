using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Interfaces.Services.Locations.Tickets;
using ETechParking.Domain.Models.Locations;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Domain.Services.Locations.Tickets;

public abstract class BaseTicketCalculationStrategy : ITicketCalculationStrategy
{
    protected abstract FareType VehicleFareType { get; }

    public decimal CalculateTotalFare(Ticket ticket)
    {
        Fare fare = GetValidFare(ticket.Location);
        TimeSpan duration = ticket.ExitDateTime!.Value - ticket.EntryDateTime;

        if (IsWithinFirstHourGrace(duration, fare.EnterGracePeriod))
            return 0m;

        return CalculateStrictHourlyFare(ticket.EntryDateTime, ticket.ExitDateTime.Value, fare);
    }

    protected Fare GetValidFare(Location location)
    {
        return location.Fares.FirstOrDefault(f => f.FareType == VehicleFareType)
               ?? throw new InvalidOperationException($"{VehicleFareType} fare not configured");
    }

    protected virtual bool IsWithinFirstHourGrace(TimeSpan duration, int graceMinutes)
    {
        return duration.TotalHours <= 1 && duration.TotalMinutes <= graceMinutes;
    }

    protected decimal CalculateStrictHourlyFare(DateTime entryTime, DateTime exitTime, Fare fare)
    {
        decimal totalFare = 0m;
        DateTime currentPeriodStart = entryTime;

        while (currentPeriodStart < exitTime)
        {
            DateTime periodEnd = Get24HourPeriodEnd(currentPeriodStart, exitTime);
            TimeSpan periodDuration = periodEnd - currentPeriodStart;

            int billableHours = CalculateExactHours(periodDuration);

            if (fare.MaxLimit.HasValue)
                billableHours = Math.Min(billableHours, fare.MaxLimit.Value);

            if (billableHours > 0)
            {
                totalFare += CalculatePeriodCharge(billableHours, fare);
            }

            currentPeriodStart = periodEnd;
        }

        return totalFare;
    }

    protected virtual decimal CalculatePeriodCharge(int billableHours, Fare fare)
    {
        return fare.FirstHourAmount + (billableHours - 1) * fare.Amount;
    }

    protected DateTime Get24HourPeriodEnd(DateTime start, DateTime exitTime)
    {
        DateTime dailyEnd = start.AddHours(24);
        return exitTime < dailyEnd ? exitTime : dailyEnd;
    }

    protected virtual int CalculateExactHours(TimeSpan duration)
    {
        int fullHours = (int)duration.TotalHours;
        int remainingMinutes = duration.Minutes;

        return remainingMinutes > 0 ? fullHours + 1 : fullHours;
    }
}
