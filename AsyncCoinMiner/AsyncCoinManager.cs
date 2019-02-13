using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCoinMiner
{
    public class AsyncCoinManager
    {
        public string MineAsyncCoinsWithPrimes(int howMany)
        {
            double allCoins = 0;
            for (int x = 2; x < howMany * 50000; x++)
            {
                int primeCounter = 0;
                for (int y = 1; y < x; y++)
                {
                    if (x % y == 0)
                    {
                        primeCounter++;
                    }

                    if(primeCounter == 2) break;
                }
                if(primeCounter != 2)
                {
                    allCoins += .01;
                }

                primeCounter = 0;
            }
            var infoString = $"Found {allCoins} Async Coin with primes";
            infoString += $" Worker Thread ID: {Thread.CurrentThread.ManagedThreadId}";
            return infoString;
        }

        public string MineAsyncCoinsWithNthRoot(int howMany)
        {
            double allCoins = 0;
            for(int i = 1; i < howMany * 2500; i++)
            {
                for(int j = 0; j <= i; j++)
                {
                    Math.Pow(i, 1.0 / j);
                    allCoins += .00001;
                }
            }
            var infoString = $"Found {allCoins} Async Coin with roots";
            infoString += $" Worker Thread ID: {Thread.CurrentThread.ManagedThreadId}";
            return infoString;
        }

        public void Execute()
        {
            // Tasks array
            var miningTasks = new Task<string>[2];

            Console.WriteLine($"Started mining at {DateTime.UtcNow}");
            Console.WriteLine($"Primary Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            miningTasks[0] = Task.Run(() => MineAsyncCoinsWithNthRoot(2));
            Console.WriteLine($"Working on some other task on thread {Thread.CurrentThread.ManagedThreadId} while the mining code runs.");
            
            miningTasks[1] = Task.Run(() => MineAsyncCoinsWithPrimes(2));
            Console.WriteLine($"And another task on thread {Thread.CurrentThread.ManagedThreadId} while the mining code runs.");

            Task.WaitAll(miningTasks);
            foreach(var task in miningTasks)
            {
                Console.WriteLine(task.Result);
            }

            Console.WriteLine($"Finished mining at {DateTime.UtcNow}");
        }
    }
}