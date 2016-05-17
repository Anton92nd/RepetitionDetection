using System;

namespace RepetitionDetection.Commons
{
    public class InvalidUsageException : Exception
    {
        public InvalidUsageException(string message) : base(message)
        {
        }
    }
}
