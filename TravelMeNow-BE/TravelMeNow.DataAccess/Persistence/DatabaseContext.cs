using TravelMeNow.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TravelMeNow.DataAccess.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options) { }

    public DbSet<PointOfInterest> PointsOfInterest { get; set; }

    public DbSet<OpeningHour> OpeningHours { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

}
