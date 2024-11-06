using System.Text.Json.Serialization;

namespace BuildingMaterials.Models;

public class MaterialModel
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("category")] public string? Category { get; set; }

    [JsonPropertyName("price")] public double Price { get; set; }

    [JsonPropertyName("weight")] public double Weight { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Category: {Category}, Price: {Price:C}, Weight: {Weight} kg";
    }
}