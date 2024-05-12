using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data.Models;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Data.DbContexts;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<ParkingZone> ParkingZones { get; set; }
    public DbSet<ParkingSlot> ParkingSlots { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Reservation>()
        .HasOne<AppUser>()
        .WithMany(u => u.Reservations)
        .HasForeignKey(r => r.AppUserId);

        base.OnModelCreating(builder);
    }
}