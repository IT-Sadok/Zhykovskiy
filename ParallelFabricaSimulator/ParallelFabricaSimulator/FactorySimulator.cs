namespace ParallelFabricaSimulator;

internal class FabricaSimulator
{
    private int _storage;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task Run()
    {
        var tasks = new List<Task>();

        for (var i = 0; i < 500; i++)
        {
            tasks.Add(AddToStorageAsync());
            tasks.Add(RemoveFromStorageAsync());
        }

        await Task.WhenAll(tasks);

        Console.WriteLine($"Final storage value: {_storage}");
    }

    private async Task AddToStorageAsync()
    {
        try
        {
            await _semaphore.WaitAsync();
            await Task.Delay(10);
            ++_storage;
        }
        finally
        {
            _semaphore.Release();
        }

        Console.WriteLine(_storage);
    }

    private async Task RemoveFromStorageAsync()
    {
        try
        {
            while (_storage <= 0)
            {
                Console.WriteLine("Waiting for storage to have some value");
                await Task.Delay(10);
            }

            await _semaphore.WaitAsync();
            --_storage;
        }
        finally
        {
            _semaphore.Release();
        }

        Console.WriteLine(_storage);
    }
}