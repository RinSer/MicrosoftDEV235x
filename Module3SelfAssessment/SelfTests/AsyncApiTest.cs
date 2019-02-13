using System;
using System.Threading.Tasks;
using Xunit;
using SelfLibrary;

namespace SelfTests
{
    public class AsyncApiTest
    {
        private readonly Uri _url;
        private readonly Uri _urlException;
        private readonly AsyncApiManager _apiManager;
        
        public AsyncApiTest()
        {
            _url = new Uri("http://validate.jsontest.com/?json=%7B%22key%22:%22value%22%7D");
            _urlException = new Uri("http://validate.jsontest.com/?json=%7B%22key:%22value%22%7D");
            _apiManager = new AsyncApiManager();
        }
        
        [Fact]
        public async void ShouldReturnStringForUrl()
        {
            var result = await _apiManager.ValidateJsonInUri(_url);
            Assert.Contains("\"validate\": true", result);
        }

        [Fact]
        public async void ShouldThrowExceptionForBadUrl()
        {
            Func<Task> test = () => _apiManager.ValidateJsonInUri(_urlException);
            await Assert.ThrowsAsync<Exception>(test);
        }
    }
}
