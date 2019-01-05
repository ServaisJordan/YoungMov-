using System;
using System.Collections.Generic;
using DTO.Global;

namespace DTO.UserControllerDTO
{
    public class UserDTORegistration : UserGlobal
    {
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
