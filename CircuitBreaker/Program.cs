using Polly;
using Polly.CircuitBreaker;

namespace CircuitBreaker;

public static class Program
{
    private static async Task Main()
    {
        HttpClient httpClient = new();

        // Circuit breaker: opened after 2 failures in 5 seconds
        AsyncCircuitBreakerPolicy? breaker = Policy
            .Handle<HttpRequestException>()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(5));

        for (int i = 0; i < 5; i++)
        {
            try
            {
                await breaker.ExecuteAsync(async () =>
                {
                    Console.WriteLine("Calling API...");
                    // Simulace selhání
                    HttpResponseMessage response = await httpClient.GetAsync("https://localhost:5001/fail");
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine("API OK");
                });
            }
            catch (BrokenCircuitException)
            {
                Console.WriteLine("Circuit breaker opened - blocking request");
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("API failed");
            }

            await Task.Delay(1000);
        }
    }
}