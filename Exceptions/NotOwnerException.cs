using System;

namespace Exceptions
{
    public class NotOwnerException : Exception
    {
        public string ErrorMessage { get; set; }
        public NotOwnerException(string errorMessage) {
            ErrorMessage = errorMessage;
        }
    }
}
