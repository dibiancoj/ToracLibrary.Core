using Library.Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Core.Tests.ExtensionMethods
{

    public class TaskExtensionTest
    {

        #region Framework

        private static async Task<string> AsyncStub1Method()
        {
            await Task.Delay(50);

            return "Test 1";
        }

        private static async Task<string> AsyncStub2Method()
        {
            await Task.Delay(50);

            return "Test 2";
        }

        #endregion

        #region Unit Tests

        [Fact(DisplayName ="Task To Result")]
        public async Task ThenResultTest()
        {
            Assert.Equal("test 1", await AsyncStub1Method().Then(tsk => tsk.ToLower()));
        }

        [Fact(DisplayName = "Task To Another Task Which Is Awaited")]
        public async Task ThenResultAwaitContinuationTest()
        {
            Assert.Equal("Test 2", await AsyncStub1Method().Then(tsk => AsyncStub2Method()));
        }

        [Fact(DisplayName = "Configured  Task To Result")]
        public async Task ThenResultWithConfigureAwaitTest()
        {
            Assert.Equal("test 1", await AsyncStub1Method().ConfigureAwait(false).Then(tsk => tsk.ToLower()));
        }

        [Fact(DisplayName = "Configured Task To Configured Await Another Task")]
        public async Task ConfigureAwaitThenResultWithConfigureAwaitTest()
        {
            Assert.Equal("Test 2", await AsyncStub1Method().ConfigureAwait(false).Then(tsk => AsyncStub2Method().ConfigureAwait(false)));
        }

        #endregion

    }

}
