using System.ComponentModel.DataAnnotations;


namespace DTO.UserControllerDTO {
    public class LoginModel {
        [Required]
        [MinLength(4)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}