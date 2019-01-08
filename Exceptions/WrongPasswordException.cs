using System;

namespace Exceptions {
    public class WrongPasswordException : BusinessException {
        public WrongPasswordException(string password) : base(password+" don't match with the original") {}
    }
}