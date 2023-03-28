namespace Exercise.Locations.Services.Services;

using Microsoft.Data.SqlClient;

using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

using Newtonsoft.Json;

public interface IGeometryService
{
    string FeatureToGeoJson(IFeature feature);

    SqlParameter LatLongToParameter(string parameterName, double latitude, double longitude);
}

public class GeometryService : IGeometryService
{
    private readonly JsonSerializer serializer = GeoJsonSerializer.Create();

    public string FeatureToGeoJson(IFeature feature)
    {
        var collection = new FeatureCollection
        {
            feature
        };

        using var stringWriter = new StringWriter();
        using var jsonWriter = new JsonTextWriter(stringWriter);
        this.serializer.Serialize(jsonWriter, collection);

        return stringWriter.ToString();
    }

    public SqlParameter LatLongToParameter(string parameterName, double latitude, double longitude)
    {
        var geometryFactory = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory();
        Geometry point = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

        var geometryWriter = new SqlServerBytesWriter { IsGeography = false };
        var geometryBytes = geometryWriter.Write(point);

        return new SqlParameter
        {
            ParameterName = parameterName,
            Value = geometryBytes,
            SqlDbType = System.Data.SqlDbType.Udt,
            UdtTypeName = "Geometry"
        };
    }
}
