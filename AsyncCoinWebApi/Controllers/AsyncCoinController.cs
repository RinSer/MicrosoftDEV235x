using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCoinWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AsyncCoinController : Controller
    {
        // GET api/asynccoin/5
        [HttpGet("{requestedAmount}")]
        public async Task<string> GetAsync(int requestedAmount)
        {
            return await AcquireAsyncCoinAsync(requestedAmount);
        }

        private async Task<string> ConnectToCoinServiceAsync(int requestedAmount)
        {
            using(var client = new HttpClient())
            {
                var uri = new Uri($"https://asynccoinfunction.azurewebsites.net/api/asynccoin/{requestedAmount}");
                return await client.GetStringAsync(uri);
            }
        }

        public async Task<string> AcquireAsyncCoinAsync(int requestedAmount)
        {
            var msg = string.Empty;
            msg += $"Your mining operation started at {DateTime.Now}" + Environment.NewLine;
            var result = await ConnectToCoinServiceAsync(requestedAmount);
            msg += $"result: {result}" + Environment.NewLine;
            msg += $"Your mining operation finished at {DateTime.Now}";
            
            return msg;
        }
    }
}