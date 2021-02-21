using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public class AttemptResult
    {
        public ResultState Result { get; init; }

        public string FailureMessage { get; init; }

        private AttemptResult()
        {
        }

        public static AttemptResult Success()
        {
            return new AttemptResult()
            {
                Result = ResultState.Success
            };
        }

        public static AttemptResult Fail(string failMessage)
        {
            return new AttemptResult()
            {
                Result = ResultState.Fail,
                FailureMessage = failMessage
            };
        }

        public static implicit operator bool(AttemptResult result)
        {
            return result != null && result.Result == ResultState.Success;
        }
    }

    public enum ResultState
    {
        Fail,
        Success
    }
}