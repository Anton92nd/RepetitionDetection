using System;

namespace GraphicalInterface
{
    public class InputDataException : Exception
    {
        public InputDataException(string message)
            : base(message)
        {
        }
    }
}