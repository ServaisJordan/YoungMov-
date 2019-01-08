using System;

namespace Exceptions {
    public class TooMuchParticipantsException : BusinessException {
        public TooMuchParticipantsException(int carpoolingId) : base("carpooling "+carpoolingId+" has already too much participant") {}
    }
}