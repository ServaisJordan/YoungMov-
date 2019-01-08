using System.ComponentModel.DataAnnotations;


namespace DTO.UserControllerDTO {
    public class LoginModel {
        [Required]
        [MinLength(4, ErrorMessage = "user name too short")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "need a password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "need a role")]
        public string Role { get; set; }
    }
}