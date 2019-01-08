using System;
using System.Collections.Generic;
using DTO.Global;
using System.ComponentModel.DataAnnotations;


namespace DTO.UserControllerDTO
{
    public class UserDTORegistration : UserGlobal
    {
        [Required(ErrorMessage = "role is missing")]
        public string Role { get; set; }
        [Required(ErrorMessage = "password is missing")]
        public string Password { get; set; }
    }
}
