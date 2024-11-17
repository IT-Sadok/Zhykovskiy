namespace ParallelFabricaSimulator;

internal class FabricaSimulator
{
    private int _storage;
    private readonly object _lockObj = new();
    private readonly SemaphoreSlim _storageSemaphore = new(0);

    public async Task Run()
    {
        var tasks = new List<Task>();

        for (var i = 0; i < 500; i++) tasks.Add(AddToStorageAsync());

        for (var i = 0; i < 500; i++) tasks.Add(RemoveFromStorageAsync());

        await Task.WhenAll(tasks);

        Console.WriteLine($"Final storage value: {_storage}");
    }

    private async Task AddToStorageAsync()
    {
        await Task.Delay(10);
        lock (_lockObj)
        {
            _storage++;
        }

        _storageSemaphore.Release();

        Console.WriteLine(_storage);
    }

    private async Task RemoveFromStorageAsync()
    {
        await _storageSemaphore.WaitAsync();
        lock (_lockObj)
        {
            _storage--;
        }

        Console.WriteLine(_storage);
    }
}