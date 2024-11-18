using System.Diagnostics;
using ParallelFabricaSimulator;

var stopwatch = Stopwatch.StartNew();

var factorySimulator = new FabricaSimulator();

Console.WriteLine("Starting simulation");

await factorySimulator.Run();

stopwatch.Stop();
Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");

Console.WriteLine("Done");