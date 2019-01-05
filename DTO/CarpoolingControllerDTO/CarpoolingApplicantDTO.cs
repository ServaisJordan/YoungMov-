using DTO.Global;

namespace DTO.CarpoolingControllerDTO {
    public class CarpoolingApplicantDTO : CarpoolingApplicantGlobal {
        public UserDTO userNavigation { get; set; }
    }
}