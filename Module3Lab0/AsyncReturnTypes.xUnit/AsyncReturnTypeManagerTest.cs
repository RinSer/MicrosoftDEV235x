using System;
using Xunit;
using AsyncReturnTypeLibrary;
using System.Threading.Tasks;

namespace AsyncReturnTypes.xUnit
{
    public class AsyncReturnTypeManagerTest
    {
        private AsyncReturnTypeManager _returnTypeMgr;
        private AsyncExceptionManager _exceptionMgr;

        public AsyncReturnTypeManagerTest()
        {
            _returnTypeMgr = new AsyncReturnTypeManager();
            _exceptionMgr = new AsyncExceptionManager();
        }

        [Fact]
        public async void AsyncManagerCanPause5SecondsWithTask()
        {
            var startTime = DateTime.UtcNow;
            await _returnTypeMgr.PauseFiveSecondsAsync();
            var diffInSeconds = (DateTime.UtcNow - startTime).TotalSeconds;
            Assert.True(diffInSeconds >= 5, "Did not pause for at least five seconds");
        }

        [Fact]
        public void AsyncManagerCantPause6SecondsWithVoid()
        {
            var startTime = DateTime.UtcNow;
            _returnTypeMgr.PauseSixSecondsAsync();
            var diffInSeconds = (DateTime.UtcNow - startTime).TotalSeconds;
            Assert.True(diffInSeconds < 5, "The void method paused for five seconds or more");
        }

        [Fact]
        public async Task AsyncManagerCanCallWebService()
        {
            var startTime = DateTime.UtcNow;
            await _returnTypeMgr.CallWebServiceThatDoesNotReturnResults();
            var diffInSeconds = (DateTime.UtcNow - startTime).TotalSeconds;
            Assert.True(diffInSeconds >= 2, "Did not mine for at least two seconds");
        }

        [Fact]
        public async Task AsyncManagerCanCallWebServiceWithResult()
        {
            string resultString = await _returnTypeMgr.CallWebServiceThatReturnsString();
            Assert.True(resultString.Contains("AsyncCoin"), "Did not return correct results");
        }

        [Fact]
        public async Task AsyncManagerCanCaptureChangedCoinPrice()
        {
            var initialPrice = await _returnTypeMgr.GetCurrentCoinPrice();
            var cachedPrice = await _returnTypeMgr.GetCurrentCoinPrice();
            await Task.Delay(3500);
            var changedPrice = await _returnTypeMgr.GetCurrentCoinPrice();
            Assert.Equal(initialPrice, cachedPrice);
            Assert.NotEqual(cachedPrice, changedPrice);
        }

        [Fact]
        public void AsyncManagerCanReturnParallelResult()
        {
            int numberOfSeconds = _returnTypeMgr.MineAndGetAggregateSeconds(5);
            Assert.True(numberOfSeconds > 2, "The Parallel For loop did not mine for more than 2 seconds");
        }

        [Fact]
        public void ExceptionManagerCanMineCoins()
        {
            var result = _exceptionMgr.MineCoinFromForbiddenServer();
            Assert.True(result.Contains("Success"), "Failed to mine.");
        }

        [Fact]
        public async Task ExceptionManagerCanMineCoinsAsync()
        {
            var result = await _exceptionMgr.MineCoinFromForbiddenServerAsync();
            Assert.True(result.Contains("Success"), "Failed to mine.");
            return;
        }

        [Fact]
        public void ExceptionManagerCanMineCoinsTaskParallel()
        {
            var result = _exceptionMgr.MineOnSeveralServers();
            Assert.True(result.Contains("Success"), "Failed to mine.");
        }

        [Fact]
        public void ExceptionManagerCanMineCoinsParallelFor()
        {
            var result = _exceptionMgr.MineForCoinsWithParallelFor();
            Assert.True(result > 0, "Did not mine any coins");
        }
    }
}