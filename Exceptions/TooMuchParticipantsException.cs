using System;

namespace Exceptions {
    public class TooMuchParticipantsException : BusinessException {
        public TooMuchParticipantsException(string ErrorMessage) : base(ErrorMessage) {}
    }
}