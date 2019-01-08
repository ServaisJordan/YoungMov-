using System;

namespace Exceptions
{
    public class NotValidatedException : BusinessException
    {
        public NotValidatedException(int carId) : base("car with id " + carId + " is not validated yet") {}
    }
}