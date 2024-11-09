using System.Text.Json.Serialization;

namespace BuildingMaterials.Models;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public class MaterialModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Category { get; set; }

    public double Price { get; set; }

    public double Weight { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Category: {Category}, Price: {Price:C}, Weight: {Weight} kg";
    }
}