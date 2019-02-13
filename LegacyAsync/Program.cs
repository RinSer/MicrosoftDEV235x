using System;
using System.Threading.Tasks;

namespace LegacyAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("start");
            var manager = new IntegrationManager();
            await manager.ExecuteAsync();
            Console.ReadLine();
        }
    }
}
