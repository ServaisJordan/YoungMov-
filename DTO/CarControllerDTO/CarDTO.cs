using DTO.Global;

namespace DTO.CarControllerDTO {
    public class CarDTO : CarGlobal {
        public UserDTO OwnerNavigation { get; set; }
    }
}