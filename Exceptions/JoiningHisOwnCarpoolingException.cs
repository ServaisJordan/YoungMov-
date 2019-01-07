using System;

namespace Exceptions {
    public class JoiningHisOwnCarpoolingException : BusinessException {
        public JoiningHisOwnCarpoolingException(string errorMessage) : base(errorMessage) {}
    }
}