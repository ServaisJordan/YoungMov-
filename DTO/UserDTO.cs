using System;
using System.Collections.Generic;

namespace DTO
{
    public partial class UserDTO : UserGlobal
    {
        public UserDTO()
        {
            Car = new HashSet<CarDTO>();
            Carpooling = new HashSet<CarpoolingDTO>();
            //CarpoolingApplicant = new HashSet<CarpoolingApplicantDTO>();
            PrivateMessage = new HashSet<PrivateMessageDTO>();
            TrustedCarpoolingDriverCarpoolerNavigation = new HashSet<TrustedCarpoolingDriverDTO>();
            TrustedCarpoolingDriverUserNavigation = new HashSet<TrustedCarpoolingDriverDTO>();
        }
        public ICollection<CarDTO> Car { get; set; }
        public ICollection<CarpoolingDTO> Carpooling { get; set; }
        //public ICollection<CarpoolingApplicantDTO> CarpoolingApplicant { get; set; }
        public ICollection<PrivateMessageDTO> PrivateMessage { get; set; }
        public ICollection<TrustedCarpoolingDriverDTO> TrustedCarpoolingDriverCarpoolerNavigation { get; set; }
        public ICollection<TrustedCarpoolingDriverDTO> TrustedCarpoolingDriverUserNavigation { get; set; }
    }
}
