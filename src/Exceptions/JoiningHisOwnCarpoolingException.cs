using System;

namespace Exceptions {
    public class JoiningHisOwnCarpoolingException : BusinessException {
        public JoiningHisOwnCarpoolingException(string userName) : base(userName+" unauthorized to join his own carpooling") {}
    }
}