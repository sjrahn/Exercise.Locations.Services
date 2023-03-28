namespace Exercise.Locations.Services.Services;

using Exercise.Locations.Services.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using NetTopologySuite.IO.Converters;

using System.Text.Json.Serialization;

public static class ApplicationBuilderService
{
    public static WebApplicationBuilder ConfigureApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureDatabase()
               .ConfigureJson()
               .ConfigureSwagger();

        builder.Services.AddScoped<ILocationService, LocationService>();
        builder.Services.AddScoped<IGeometryService, GeometryService>();

        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LocationsContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LocationsContext") ?? throw new InvalidOperationException("Connection string 'LocationsContext' not found."),
            x => x.UseNetTopologySuite()));

        return builder;
    }

    private static WebApplicationBuilder ConfigureJson(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            var geoJsonConverterFactory = new GeoJsonConverterFactory();
            options.SerializerOptions.Converters.Add(geoJsonConverterFactory);
            options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
        });

        return builder;
    }

    private static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocationApi", Version = "v1" });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
}
