public static class Program
{
    public static async Task Main(string[] args)
    {
        SemaphoreSlim semaphore = new(3);
        IEnumerable<int> range = Enumerable.Range(1, 12);
        
        await Task.WhenAll(range.Select(number => ProcessAsync(number, semaphore)));
    }
    
    private static  async Task ProcessAsync(int id, SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync();
        try
        {
            Console.WriteLine($"Task {id} started as {DateTime.Now:T}");
            await Task.Delay(1000); // Simulate work
        }
        finally
        {
            semaphore.Release();
        }
    }
}
