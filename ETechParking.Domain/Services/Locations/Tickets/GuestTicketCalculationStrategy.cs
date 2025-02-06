using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Interfaces.Services.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Domain.Services.Locations.Tickets;

public class GuestTicketCalculationStrategy : ITicketCalculationStrategy
{
    public decimal CalculateTotalFare(Ticket ticket)
    {
        Fare fare = ticket?.Location?.Fares.FirstOrDefault(f => f.FareType == FareType.Daily)!
            ?? throw new InvalidOperationException("No daily fare found for the guest.");
        TimeSpan duration = ticket!.ExitDateTime!.Value - ticket.EntryDateTime;

        if (duration.TotalDays < 1)
        {
            return 0m;
        }

        DateTime nextDayStart = ticket.EntryDateTime.Date.AddDays(1);
        TimeSpan remainingDuration = ticket.ExitDateTime!.Value - nextDayStart;

        int days = CalculateDaysWithGracePeriod(remainingDuration, fare.ExitGracePeriod);

        return days * fare.Amount;
    }

    private static int CalculateDaysWithGracePeriod(TimeSpan duration, int exitGracePeriod)
    {
        int totalDays = (int)duration.TotalDays;
        int remainingHours = duration.Hours;

        if (remainingHours > exitGracePeriod)
        {
            totalDays += 1;
        }

        return totalDays;
    }
}
