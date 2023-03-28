namespace Exercise.Locations.Services.Models;

using NetTopologySuite.Geometries;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class LuklaLocation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LocationId { get; init; }

    public required string GeoJson { get; init; }

    [Column(TypeName = "geometry")]
    public required Geometry Geometry { get; init;}
}