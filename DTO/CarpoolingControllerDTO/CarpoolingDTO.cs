using DTO.Global;
using System.Collections.Generic;

namespace DTO.CarpoolingControllerDTO {
    public class CarpoolingDTO : CarpoolingGlobal {

        public CarpoolingDTO() {
            CarpoolingApplicant = new HashSet<CarpoolingApplicantDTO>();
        }
        public CarDTO CarNavigation { get; set; }
        public ICollection<CarpoolingApplicantDTO> CarpoolingApplicant { get; set; }
        public UserDTO CreatorNavigation { get; set; }

    }
}