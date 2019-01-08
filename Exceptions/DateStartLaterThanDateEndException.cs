using System;

namespace Exceptions {
    public class DateStartLaterThanDateEndException : BusinessException {
        public DateStartLaterThanDateEndException(DateTime dateStart, DateTime dateEnd) : base(dateStart+" is later than "+dateEnd) {}
    }
}