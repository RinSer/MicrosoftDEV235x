using System;
using System.Net;
using System.Threading.Tasks;


namespace Module4SelfAssessment
{
    class Eap2Tap
    {
        public void Execute()
        {
            int cp;
            Console.WriteLine(SellSomeCoin("0", 5, out cp));
        }
        
        public async Task ExecuteAsync()
        {
            Console.WriteLine(await SellSomeCoinAsync("0", 5));
        }

        public Task<string> SellSomeCoinAsync(string authToken, int howMany)
        {
            if(string.IsNullOrEmpty(authToken))
            {
                throw new Exception("Failed Authorization");
            }
            TaskCompletionSource<string> taskResult = new TaskCompletionSource<string>();
            var webClient = new WebClient();
            webClient.DownloadStringCompleted += (s, e) => {
                if (e.Error != null)
                    throw new Exception(e.Error.Message);
                else
                {
                    string result = e.Result.ToString();
                    taskResult.SetResult(result);
                }
            };
            var uri = new Uri($"https://asynccoinfunction.azurewebsites.net/api/sellcoin/{howMany}");
            webClient.DownloadStringAsync(uri);
            return taskResult.Task;
        }

        public string SellSomeCoin(string authToken, int howMany, out int currentMarketPrice)
        {
            if(string.IsNullOrEmpty(authToken))
            {
                throw new Exception("Failed Authorization");
            }
            currentMarketPrice = new Random().Next(50, 120);
            var uri = new Uri($"https://asynccoinfunction.azurewebsites.net/api/sellcoin/{howMany}");
            var webClient = new WebClient();
            var result = webClient.DownloadString(uri);
            return result;
        }
    }
}
