namespace Exercise.Locations.Services.Controllers;

using Exercise.Locations.Services.Models;
using Exercise.Locations.Services.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis;

using NetTopologySuite.Features;


public static class LocationEndpoints
{
    public static void MapLocationEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Location").WithTags(nameof(LuklaLocation));

        group.MapGet("/", Results<Ok<string>, NotFound> (double latitude, double longitude, double distance, ILocationService service) =>
        {
            IEnumerable<LuklaLocation> results = service.GetLocationByDistance(latitude, longitude, distance);

            return results.Any()
                ? TypedResults.Ok(results.First().GeoJson)
                : TypedResults.NotFound();
        })
        .WithName("GetLocationByDistance")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<string>, NotFound>> (int id, ILocationService service) =>
        {
            return await service.GetLocationById(id)
                is LuklaLocation model
                    ? TypedResults.Ok(model.GeoJson)
                    : TypedResults.NotFound();
        })
        .WithName("GetLocationById")
        .WithOpenApi();

        group.MapPost("/", async Task<Results<Created, BadRequest>> (FeatureCollection features, ILocationService service) =>
        {
            int recordCount = await service.AddLocations(features);

            return recordCount != 0
                ? TypedResults.Created($"{recordCount} location(s) written to database")
                : TypedResults.BadRequest();
        })
        .WithName("AddLocations")
        .WithOpenApi();
    }
}
