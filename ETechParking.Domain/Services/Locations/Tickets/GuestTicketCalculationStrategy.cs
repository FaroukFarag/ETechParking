using ETechParking.Domain.Enums.Locations.Fares;
using ETechParking.Domain.Interfaces.Services.Locations.Tickets;
using ETechParking.Domain.Models.Locations.Fares;
using ETechParking.Domain.Models.Locations.Tickets;

namespace ETechParking.Domain.Services.Locations.Tickets;

public class GuestTicketCalculationStrategy : ITicketCalculationStrategy
{
    public decimal CalculateTotalFare(Ticket ticket)
    {
        if (ticket == null || ticket.Location == null || ticket.ExitDateTime == null)
        {
            throw new InvalidOperationException("Invalid ticket data.");
        }

        Fare fare = ticket.Location.Fares.FirstOrDefault(f => f.FareType == FareType.Daily)
            ?? throw new InvalidOperationException("No daily fare found for the guest.");

        if (ticket.NumberOfDays.HasValue && ticket.NumberOfDays > 0)
        {
            return CalculateMultiDayFare(ticket, fare);
        }

        return CalculateSingleDayFare(ticket, fare);
    }

    private static decimal CalculateMultiDayFare(Ticket ticket, Fare fare)
    {
        int numberOfDays = ticket.NumberOfDays!.Value;
        decimal totalFare = numberOfDays * fare.Amount;
        DateTime ticketExpiry = ticket.EntryDateTime.AddDays(numberOfDays);

        if (ticket.ExitDateTime > ticketExpiry)
        {
            TimeSpan overstayDuration = ticket.ExitDateTime.Value - ticketExpiry;

            if (overstayDuration.TotalHours > fare.ExitGracePeriod)
            {
                int extraDays = (int)Math.Ceiling((overstayDuration.TotalHours - fare.ExitGracePeriod) / 24);
                totalFare += extraDays * fare.Amount;
            }
        }

        return totalFare;
    }

    private static decimal CalculateSingleDayFare(Ticket ticket, Fare fare)
    {
        TimeSpan duration = ticket.ExitDateTime!.Value - ticket.EntryDateTime;
        if (duration.TotalDays < 1)
        {
            return fare.Amount;
        }

        DateTime nextDayStart = ticket.EntryDateTime.Date.AddDays(1);
        TimeSpan remainingDuration = ticket.ExitDateTime.Value - nextDayStart;

        int totalDays = CalculateDaysWithGracePeriod(remainingDuration, fare.ExitGracePeriod);

        return totalDays * fare.Amount;
    }

    private static int CalculateDaysWithGracePeriod(TimeSpan duration, int gracePeriodHours)
    {
        int totalDays = (int)duration.TotalDays;
        double remainingHours = duration.TotalHours;

        if (remainingHours > (double)gracePeriodHours)
        {
            totalDays += 1;
        }

        return totalDays;
    }
}
