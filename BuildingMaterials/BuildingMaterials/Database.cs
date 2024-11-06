using System.Text.Json;
using BuildingMaterials.Models;

namespace BuildingMaterials;

public class Database
{
    private const string DbFilePath = "db.json";

    public List<MaterialModel> Materials { get; }

    public Database()
    {
        if (!File.Exists(DbFilePath)) File.WriteAllText(DbFilePath, "[]");

        var json = File.ReadAllText(DbFilePath);
        Materials = JsonSerializer.Deserialize<List<MaterialModel>>(json) ?? [];
    }

    public int AddMaterial(MaterialModel material)
    {
        var maxId = Materials.Count != 0 ? Materials.Max(m => m.Id) : 0;

        material.Id = maxId + 1;
        Materials.Add(material);

        SaveChanges();
        return material.Id;
    }

    public bool UpdateMaterial(int id, MaterialModel material)
    {
        var index = Materials.FindIndex(m => m.Id == id);
        if (index == -1) return false;

        material.Id = id;
        Materials[index] = material;

        SaveChanges();
        return true;
    }

    public bool DeleteMaterial(int id)
    {
        var result = Materials.RemoveAll(m => m.Id == id);
        SaveChanges();
        return result > 0;
    }

    private void SaveChanges()
    {
        var json = JsonSerializer.Serialize(Materials, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(DbFilePath, json);
    }
}