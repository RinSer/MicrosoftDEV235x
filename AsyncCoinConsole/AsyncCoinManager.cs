using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace AsyncCoinConsole
{
    public class AsyncCoinManager
    {
        private async Task<string> ConnectToCoinServiceAsync(int amount)
        {
            using(var client = new HttpClient())
            {
                var uri = new Uri($"https://asynccoinfunction.azurewebsites.net/api/asynccoin/{amount}");
                return await client.GetStringAsync(uri);
            }
        }

        public async Task AcquireAsyncCoinAsync(int amount)
        {
            Console.WriteLine($"Start call to long-running service at {DateTime.Now}");
            var result = await ConnectToCoinServiceAsync(amount);
            var savedColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"result: {result}");
            Console.ForegroundColor = savedColor;
            Console.WriteLine($"Finish call to long-running service at {DateTime.Now}");
        }
    }
}