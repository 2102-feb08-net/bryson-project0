using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class AttemptResultTests
    {
        [Fact]
        public void AttemptResult_Success_IsTrue()
        {
            // arrange

            // act
            bool value = AttemptResult.Success();

            // assert
            Assert.True(value);
        }

        [Fact]
        public void AttemptResult_Fail_IsFalse()
        {
            // arrange

            // act
            bool value = AttemptResult.Fail("This is a failure.");

            // assert
            Assert.False(value);
        }

        [Fact]
        public void AttemptResult_FailNullMessage_ArgumentNullException()
        {
            // arrange

            // act
            static void GetFail() => AttemptResult.Fail(null);

            // assert
            Assert.Throws<ArgumentNullException>(GetFail);
        }

        [Fact]
        public void AttemptResult_FailEmptyMessage_ArgumentNullException()
        {
            // arrange

            // act
            static void GetFail() => AttemptResult.Fail("      ");

            // assert
            Assert.Throws<ArgumentException>(GetFail);
        }
    }
}