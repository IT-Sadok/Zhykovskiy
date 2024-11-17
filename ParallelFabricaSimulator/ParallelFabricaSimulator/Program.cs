using ParallelFabricaSimulator;

var factorySimulator = new FabricaSimulator();

Console.WriteLine("Starting simulation");

await factorySimulator.Run();

Console.WriteLine("Done");