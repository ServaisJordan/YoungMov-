using System;
using System.Collections.Generic;

namespace DTO
{
    public partial class UserDTORegistration : UserGlobal
    {
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
