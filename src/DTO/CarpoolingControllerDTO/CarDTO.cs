using System;
using DTO.Global;

namespace DTO.CarpoolingControllerDTO {
    public class CarDTO : CarGlobal {
        public UserDTO OwnerNavigation { get; set; }
    }
}