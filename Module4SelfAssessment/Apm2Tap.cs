using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;


namespace Module4SelfAssessment
{
    class Apm2Tap
    {
        public async Task ExecuteAsync()
        {
            await CoinSalesAsync(5);
        }

        public Task CoinSalesAsync(int howMany)
        {
            TaskCompletionSource<string> result = new TaskCompletionSource<string>();
            string url = $"https://asynccoinfunction.azurewebsites.net/api/sellcoin/{howMany}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.BeginGetResponse(asyncResult => {
                try
                {
                    HttpWebResponse response = (asyncResult.AsyncState as HttpWebRequest).EndGetResponse(asyncResult) as HttpWebResponse;
                    string salesResult;
                    using (StreamReader httpWebStreamReader = new StreamReader(response.GetResponseStream()))
                    {
                        salesResult = httpWebStreamReader.ReadToEnd();
                    }
                    var marketPrice = new Random().Next(50, 120);
                    Console.WriteLine($"Current coin price: {marketPrice}");
                    Console.WriteLine(salesResult);
                }
                catch (Exception e)
                {
                    result.SetException(e);
                }
            }, request);
            return result.Task;
        }
        
        public void Execute()
        {
            BeginApmCoinSales(5);
        }

        public void BeginApmCoinSales(int howMany)
        {
            string url = $"https://asynccoinfunction.azurewebsites.net/api/sellcoin/{howMany}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.BeginGetResponse(new AsyncCallback(EndApmCoinSales), request);
        }

        public void EndApmCoinSales(IAsyncResult result)
        {
            HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
            string salesResult;
            using (StreamReader httpWebStreamReader = new StreamReader(response.GetResponseStream()))
            {
                salesResult = httpWebStreamReader.ReadToEnd();
            }
            var marketPrice = new Random().Next(50, 120);
            Console.WriteLine($"Current coin price: {marketPrice}");
            Console.WriteLine(salesResult);
        }
    }
}
