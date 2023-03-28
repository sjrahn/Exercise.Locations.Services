namespace Exercise.Locations.Services.Data;

using Microsoft.EntityFrameworkCore;

using Exercise.Locations.Services.Models;

public class LocationsContext : DbContext
{
    public LocationsContext (DbContextOptions<LocationsContext> options)
        : base(options)
    {
    }

    public DbSet<LuklaLocation> LuklaLocation { get; set; } = default!;
}
