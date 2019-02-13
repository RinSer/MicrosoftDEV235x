using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace SelfLibrary
{
    public class AsyncApiManager
    {
        public async Task<string> ValidateJsonInUri(Uri uri)
        {
            using(var client = new HttpClient())
            {
                var response = await client.GetStringAsync(uri);
                if (response != null)
                {
                    if (response.Contains("\"validate\": false"))
                    {
                        throw new Exception("Validation failure!");
                    }
                    else
                    {
                        return response;
                    }
                }
                else
                {
                    throw new Exception("Response is empty!");
                }
            }
        }
    }
}
