using DTO.Global;

namespace DTO.CarpoolingApplicantControllerDTO {
    public class CarpoolingApplicantDTO : CarpoolingApplicantGlobal {
        public CarpoolingDTO CarpoolingNavigation { get; set; }
    }
}