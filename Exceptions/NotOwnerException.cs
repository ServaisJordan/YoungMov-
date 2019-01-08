using System;

namespace Exceptions
{
    public class NotOwnerException : BusinessException
    {
        public NotOwnerException(string userName) : base(userName+" is not the owner of this car") {
        }
    }
}
