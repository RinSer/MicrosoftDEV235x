using System;
using System.Threading.Tasks;

namespace AsyncCoinConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var manager = new AsyncCoinManager();
            Console.WriteLine("Enter the number of coins you wish to aquire...");
            var input = Console.ReadLine();
            int amount;
            if (int.TryParse(input, out amount))
            {
                await manager.AcquireAsyncCoinAsync(amount);
            }
            else
            {
                Console.WriteLine("Could not parse your int!");
            }
            Console.ReadKey();
            return;
        }
    }
}
