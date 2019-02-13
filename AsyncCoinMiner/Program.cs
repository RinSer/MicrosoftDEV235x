using System;

namespace AsyncCoinMiner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin");
            var miningManager = new AsyncCoinManager();
            miningManager.Execute();
            Console.ReadKey();
        }
    }
}
