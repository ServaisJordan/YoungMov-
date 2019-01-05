using DTO.Global;

namespace DTO.PrivateMessageControllerDTO {
    public class PrivateMessageDTO : PrivateMessageGlobal {
        public UserDTO CreatorNavigation { get; set; }
        public PrivateMessageDTO ReponseNavigation { get; set; }
    }
}