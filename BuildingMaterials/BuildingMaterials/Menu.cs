using BuildingMaterials.Models;

namespace BuildingMaterials;

public class Menu(Database database)
{
    public void RunMenuInteractions()
    {
        var running = true;

        while (running)
        {
            Console.Clear();
            PrintMenu();

            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    PrintMaterials();
                    break;
                case ConsoleKey.D2:
                    AddMaterial();
                    break;
                case ConsoleKey.D3:
                    UpdateMaterial();
                    break;
                case ConsoleKey.D4:
                    DeleteMaterial();
                    break;
                case ConsoleKey.D5:
                    running = false;
                    ExitProgram();
                    break;
                default:
                    continue;
            }

            if (!running) continue;

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }

    private static void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Show all materials");
        Console.WriteLine("2. Add new material");
        Console.WriteLine("3. Update material by id");
        Console.WriteLine("4. Delete material by id");
        Console.WriteLine("5. Exit");
    }

    private static void ExitProgram()
    {
        Console.Clear();
        Console.WriteLine("Exiting...");
    }

    private void PrintMaterials()
    {
        Console.Clear();
        if (database.Materials.Count == 0)
            Console.WriteLine("No materials available.");
        else
            foreach (var material in database.Materials)
                Console.WriteLine(material);
    }

    private MaterialModel GetMaterial()
    {
        var name = ReadString("Enter Name: ");
        var category = ReadString("Enter Category: ");
        var price = ReadDouble("Enter Price: ");
        var weight = ReadDouble("Enter Weight: ");

        return new MaterialModel { Name = name, Category = category, Price = price, Weight = weight };
    }

    private void AddMaterial()
    {
        Console.Clear();

        var id = database.AddMaterial(GetMaterial());

        Console.WriteLine($"Material added: {id}");
    }

    private void DeleteMaterial()
    {
        PrintMaterials();

        Console.Write("Enter id of the material: ");
        var id = int.TryParse(Console.ReadLine(), out var result) ? result : -1;

        Console.Clear();

        Console.WriteLine(database.DeleteMaterial(id) ? "Material removed" : "No material found");
    }

    private void UpdateMaterial()
    {
        PrintMaterials();

        Console.Write("Enter id of the material: ");
        var id = int.TryParse(Console.ReadLine(), out var result) ? result : -1;

        Console.Clear();

        var material = database.Materials.Find(x => x.Id == id);

        if (material == null)
        {
            Console.WriteLine("Material not found");
            return;
        }

        Console.WriteLine(material);

        Console.WriteLine("What parameter you want to update?");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Category");
        Console.WriteLine("3. Price");
        Console.WriteLine("4. Weight");
        Console.WriteLine("5. All parameters");
        Console.WriteLine("6. Exit");

        var runningUpdate = true;

        while (runningUpdate)
        {
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    material.Name = ReadString("Enter Name: ");
                    break;
                case ConsoleKey.D2:
                    material.Category = ReadString("Enter Category: ");
                    break;
                case ConsoleKey.D3:
                    material.Price = ReadDouble("Enter Price: ");
                    break;
                case ConsoleKey.D4:
                    material.Weight = ReadDouble("Enter Weight: ");
                    break;
                case ConsoleKey.D5:
                    material = GetMaterial();
                    break;
                case ConsoleKey.D6:
                    break;
                default:
                    continue;
            }

            runningUpdate = false;
        }

        Console.WriteLine(database.UpdateMaterial(id, material) ? "Material updated" : "Error while updating material");
    }

    private double ReadDouble(string input)
    {
        Console.Write(input);

        while (true)
        {
            if (double.TryParse(Console.ReadLine()?.Replace(".", ","), out var result))
                return result;
            Console.Write("Invalid input. Please enter a valid number: ");
        }
    }

    private string ReadString(string input)
    {
        Console.Write(input);

        return Console.ReadLine() ?? string.Empty;
    }
}