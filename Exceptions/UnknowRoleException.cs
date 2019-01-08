namespace Exceptions {
    public class UnknowRoleException : BusinessException {
        public UnknowRoleException(string role) : base (role +" is not a role") {}
    }
}