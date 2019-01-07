using System;
using System.Collections.Generic;
using DTO.Global;
using System.ComponentModel.DataAnnotations;


namespace DTO.UserControllerDTO
{
    public class UserDTORegistration : UserGlobal
    {
        [Required]
        public string Role { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
