using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;


namespace Module4SelfAssessment
{
    class TapTap
    {
        public async Task ExecuteAsync()
        {
            Console.WriteLine(await GetCoinsAsync(5));
            // With cancellation token
            var ct = new CancellationTokenSource(1000);
            Task<string> getCoinsTask = GetCoinsAsync(5, ct.Token);
            await ExecuteTryCatch(getCoinsTask);
            // With progress
            var prog = new Progress<int>(p => Console.WriteLine($"Progress: {p}"));
            var result = await GetCoinsAsync(5, prog);
            Console.WriteLine(result);
            // Both
            ct = new CancellationTokenSource(1000);
            getCoinsTask = GetCoinsAsync(5, ct.Token, prog);
            await ExecuteTryCatch(getCoinsTask);
        }

        public async Task ExecuteTryCatch(Task<string> task2execute)
        {
            try
            {
                var result = await task2execute;
                Console.WriteLine(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Status: {task2execute.Status}");
            }
        }

        public async Task<string> GetCoinsAsync(int howMany)
        {
            using(var client = new HttpClient())
            {
                var uri = new Uri($"https://asynccoinfunction.azurewebsites.net/api/sellcoin/{howMany}");
                var result = await client.GetStringAsync(uri);
                return result;
            }
        }

        public async Task<string> GetCoinsAsync(int howMany, CancellationToken cancelToken)
        {
            using(var client = new HttpClient())
            {
                var uri = new Uri($"https://asynccoinfunction.azurewebsites.net/api/sellcoin/{howMany}");
                Thread.Sleep(1500);
                if (cancelToken.IsCancellationRequested)
                {
                    cancelToken.ThrowIfCancellationRequested();
                }
                var result = await client.GetStringAsync(uri);
                return result;
            }
        }

        public async Task<string> GetCoinsAsync(int howMany, IProgress<int> progress)
        {
            using(var client = new HttpClient())
            {
                var uri = new Uri($"https://asynccoinfunction.azurewebsites.net/api/sellcoin/{howMany}");
                
                for (int i = 0; i < howMany; i++)
                {
                    await Task.Delay(1000);
                    progress.Report(i);
                }

                return await client.GetStringAsync(uri);
            }
        }

        public async Task<string> GetCoinsAsync(int howMany, CancellationToken cancelToken, IProgress<int> progress)
        {
            using(var client = new HttpClient())
            {
                var uri = new Uri($"https://asynccoinfunction.azurewebsites.net/api/sellcoin/{howMany}");
                
                for (int i = 0; i < howMany; i++)
                {
                    await Task.Delay(1000);
                    progress.Report(i);
                }
                
                if (cancelToken.IsCancellationRequested)
                {
                    cancelToken.ThrowIfCancellationRequested();
                }

                var result = await client.GetStringAsync(uri);
                return result;
            }
        }
    }
}
