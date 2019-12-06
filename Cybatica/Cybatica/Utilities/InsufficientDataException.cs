using System;
using System.Runtime.Serialization;

namespace Cybatica.Utilities
{
    [Serializable]
    public class InsufficientDataException : Exception
    {
        public InsufficientDataException()
        {
        }

        public InsufficientDataException(string message) : base(message)
        {
        }

        public InsufficientDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientDataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}