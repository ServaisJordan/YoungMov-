namespace Exceptions {
    public class NotATrustedCarpoolingDriver : BusinessException {
        public NotATrustedCarpoolingDriver(string userName) : base(userName +" is not a trusted carpooling driver") {}
    }
}