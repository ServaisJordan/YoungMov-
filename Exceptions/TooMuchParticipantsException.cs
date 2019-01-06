using System;

namespace Exceptions {
    public class TooMuchParticipantsException : Exception {
        public TooMuchParticipantsException(string ErrorMessage) : base(ErrorMessage) {}
    }
}