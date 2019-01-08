using System.ComponentModel.DataAnnotations;

namespace DTO.UserControllerDTO {
    public class NewPasswordModel {
        [Required(ErrorMessage = "id is missing")]
        public string Id { get; set; }
        [Required(ErrorMessage = "password is missing")]
        public string ActualPassword { get; set; }
        [Required(ErrorMessage = "new password is missing")]
        public string NewPassword { get; set; }
    }
}