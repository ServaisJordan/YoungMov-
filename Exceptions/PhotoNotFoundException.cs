using System;

namespace Exceptions {
    public class PhotoNotFoundException : BusinessException {
        public PhotoNotFoundException(string userName) : base(userName + " dont have photo") {}
    }
}