using System;

namespace Exceptions {
    public class JoiningHisOwnCarpoolingException : Exception {
        public JoiningHisOwnCarpoolingException(string errorMessage) : base(errorMessage) {}
    }
}