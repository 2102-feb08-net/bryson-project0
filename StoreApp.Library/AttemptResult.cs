using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    /// <summary>
    /// Essentially a boolean, but can also return a message when false (which is called Fail in this class)
    /// </summary>
    public class AttemptResult
    {
        /// <summary>
        /// The result state of the AttemptResult
        /// </summary>
        public ResultState Result { get; init; }

        /// <summary>
        /// The failure message of the AttemptResult. Only has a value if the result is a failure.
        /// </summary>
        public string FailureMessage { get; init; }

        private AttemptResult()
        {
        }

        /// <summary>
        /// Creates a new AttemptResult that is a success.
        /// </summary>
        /// <returns>Returns the AttemptResult</returns>
        public static AttemptResult Success()
        {
            return new AttemptResult()
            {
                Result = ResultState.Success
            };
        }

        /// <summary>
        /// Creates a new AttemptResult that is a failure with the specified failure message.
        /// </summary>
        /// <param name="failMessage">The failure message attached to the AttemptResult</param>
        /// <returns></returns>
        public static AttemptResult Fail(string failMessage)
        {
            if (failMessage is null)
                throw new ArgumentNullException(paramName: nameof(failMessage), message: "Fail message cannot be null.");

            if (string.IsNullOrWhiteSpace(failMessage))
                throw new ArgumentException(message: "Fail message cannot be empty or whitespace.", paramName: nameof(failMessage));

            return new AttemptResult()
            {
                Result = ResultState.Fail,
                FailureMessage = failMessage
            };
        }

        /// <summary>
        /// An implicit operator to a boolean. This is to allow easy use as a return in a TryXXX() method.
        /// </summary>
        /// <param name="result">Returns true if the result is a success, otherwise false.</param>
        public static implicit operator bool(AttemptResult result) => result is not null && result.Result == ResultState.Success;
    }

    /// <summary>
    /// A state for AttemptResult to show whether it is a fail or success.
    /// </summary>
    public enum ResultState
    {
        Fail,
        Success
    }
}