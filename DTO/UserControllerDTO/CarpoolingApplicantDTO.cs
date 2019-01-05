using DTO.Global;

namespace DTO.UserControllerDTO {
    public class CarpoolingApplicantDTO : CarpoolingApplicantGlobal {
        public CarpoolingDTO CarpoolingNavigation { get; set; }
    }
}