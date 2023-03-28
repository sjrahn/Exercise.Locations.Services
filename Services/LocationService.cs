namespace Exercise.Locations.Services.Services;

using Exercise.Locations.Services.Data;
using Exercise.Locations.Services.Models;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using NetTopologySuite.Features;

public interface ILocationService
{
    Task<int> AddLocations(FeatureCollection features);

    IEnumerable<LuklaLocation> GetLocationByDistance(double latitude, double longitude, double distance);

    Task<LuklaLocation?> GetLocationById(int locationid);
}

public class LocationService: ILocationService
{
    private readonly IGeometryService geometryService;
    private readonly LocationsContext db;

    public LocationService(IGeometryService geometryService, LocationsContext db)
    {
        this.geometryService = geometryService ?? throw new ArgumentNullException(nameof(geometryService));
        this.db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public Task<int> AddLocations(FeatureCollection features)
    {
        IEnumerable<LuklaLocation> locs = features.Select(f => new LuklaLocation
        {
            GeoJson = this.geometryService.FeatureToGeoJson(f),
            Geometry = f.Geometry
        });

        db.LuklaLocation.AddRange(locs);

        return db.SaveChangesAsync();
    }

    public IEnumerable<LuklaLocation> GetLocationByDistance(double latitude, double longitude, double distance)
    {
        var geom = this.geometryService.LatLongToParameter("@geom", latitude, longitude);
        var dist = new SqlParameter("@dist", distance);

        return db.LuklaLocation.FromSqlRaw($"SELECT * FROM dbo.GetNearestId(@geom, @dist)", geom, dist);
    }

    public Task<LuklaLocation?> GetLocationById(int id)
        => this.db.LuklaLocation.AsNoTracking().FirstOrDefaultAsync(model => model.LocationId == id);
}
