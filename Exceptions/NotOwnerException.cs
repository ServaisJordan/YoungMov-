using System;

namespace Exceptions
{
    public class NotOwnerException : BusinessException
    {
        public string ErrorMessage { get; set; }
        public NotOwnerException(string errorMessage) : base(errorMessage) {
            ErrorMessage = errorMessage;
        }
    }
}
