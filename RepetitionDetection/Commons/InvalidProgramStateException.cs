using System;

namespace RepetitionDetection.Commons
{
    public class InvalidProgramStateException : Exception
    {
        public InvalidProgramStateException(string message) : base(message)
        {          
        }
    }
}
