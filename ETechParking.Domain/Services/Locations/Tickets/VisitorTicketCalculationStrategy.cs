using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Interfaces.Services.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Domain.Services.Locations.Tickets;

public class VisitorTicketCalculationStrategy : ITicketCalculationStrategy
{
    public decimal CalculateTotalFare(Ticket ticket)
    {
        Fare fare = ticket.Location.Fares.FirstOrDefault(f => f.FareType == FareType.Hourly)!
            ?? throw new InvalidOperationException("No hourly fare found for the visitor.");
        TimeSpan duration = ticket.ExitDateTime - ticket.EntryDateTime;

        if (duration.TotalMinutes <= fare.EnterGracePeriod)
        {
            return 0m;
        }

        return CalculateVisitorFareWithMaxLimit(ticket.EntryDateTime, ticket.ExitDateTime, fare);
    }

    private decimal CalculateVisitorFareWithMaxLimit(DateTime entryDateTime, DateTime exitDateTime, Fare fare)
    {
        decimal totalFare = 0m;
        DateTime currentDayStart = entryDateTime.Date;

        while (currentDayStart < exitDateTime)
        {
            DateTime currentDayEnd = currentDayStart.AddDays(1);
            DateTime dayEntry = entryDateTime > currentDayStart ? entryDateTime : currentDayStart;
            DateTime dayExit = exitDateTime < currentDayEnd ? exitDateTime : currentDayEnd;

            TimeSpan dayDuration = dayExit - dayEntry;

            int hours = CalculateHoursWithGracePeriod(dayDuration, fare.ExitGracePeriod);

            if (fare.MaxLimit.HasValue && hours > fare.MaxLimit)
            {
                hours = fare.MaxLimit.Value;
            }

            totalFare += hours * fare.Amount;

            currentDayStart = currentDayEnd;
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
