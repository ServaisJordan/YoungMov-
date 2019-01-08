using DTO.Global;

namespace DTO.UserControllerDTO {
    public class TrustedCarpoolingDriverDTO : TrustedCarpoolingDriverGlobal {
        public UserWithOptionalProprities CarpoolerNavigation { get; set; }
        
    }
}