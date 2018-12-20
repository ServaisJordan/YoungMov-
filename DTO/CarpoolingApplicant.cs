using System;
using System.Collections.Generic;

namespace DTO
{
    public partial class CarpoolingApplicantDTO
    {
        public int Carpooling { get; set; }
        public int User { get; set; }
        public bool HasBeenAccepted { get; set; }

        public UserDTO UserNavigation { get; set; }
    }
}
