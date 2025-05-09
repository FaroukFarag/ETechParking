using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Interfaces.Services.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Domain.Services.Locations.Tickets;

public class CarTicketCalculationStrategy : ITicketCalculationStrategy
{
    public decimal CalculateTotalFare(Ticket ticket)
    {
        Fare fare = ticket.Location.Fares.FirstOrDefault(f => f.FareType == FareType.Car)!
            ?? throw new InvalidOperationException("No fare found for the car.");
        TimeSpan duration = ticket.ExitDateTime!.Value - ticket.EntryDateTime;

        if (duration.TotalMinutes <= fare.EnterGracePeriod)
        {
            return 0m;
        }

        return CalculateFareWithMaxLimit(ticket.EntryDateTime, ticket.ExitDateTime!.Value, fare);
    }

    private decimal CalculateFareWithMaxLimit(DateTime entryDateTime, DateTime exitDateTime, Fare fare)
    {
        decimal totalFare = 0m;
        DateTime currentPeriodStart = entryDateTime;

        while (currentPeriodStart < exitDateTime)
        {
            DateTime currentPeriodEnd = currentPeriodStart.AddHours(24);

            if (exitDateTime < currentPeriodEnd)
                currentPeriodEnd = exitDateTime;

            TimeSpan periodDuration = currentPeriodEnd - currentPeriodStart;

            int hours = CalculateHoursWithGracePeriod(periodDuration, fare.ExitGracePeriod);

            if (fare.MaxLimit.HasValue && hours > fare.MaxLimit)
                hours = fare.MaxLimit.Value;

            if (hours > 0)
            {
                totalFare += fare.FirstHourAmount + (hours - 1) * fare.Amount;
            }

            currentPeriodStart = currentPeriodEnd;
        }

        return totalFare;
    }

    private int CalculateHoursWithGracePeriod(TimeSpan duration, int exitGracePeriod)
    {
        int totalHours = (int)duration.TotalHours;
        int remainingMinutes = duration.Minutes;

        if (remainingMinutes > exitGracePeriod)
        {
            totalHours += 1;
        }

        return totalHours;
    }
}
