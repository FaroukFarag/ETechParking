﻿using ETechParking.Domain.Models.Locations.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETechParking.Infrastructure.Data.ModelsConfigurations.Locations.Tickets;

public class TicketConfigurations : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.Property(t => t.TicketNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.PlateNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.PhoneNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.EntryDateTime)
            .IsRequired();

        builder.Property(t => t.ClientType)
            .IsRequired();

        builder.Property(f => f.TotalAmount)
            .HasPrecision(18, 6);

        builder.HasOne(t => t.Location)
            .WithMany(l => l.Tickets)
            .HasForeignKey(t => t.LocationId)
            .IsRequired();

        builder.HasOne(t => t.CreateUser)
            .WithMany(u => u.CreatedTickets)
            .HasForeignKey(t => t.CreateUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.CloseUser)
            .WithMany(u => u.ClosedTickets)
            .HasForeignKey(t => t.CloseUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
