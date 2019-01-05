using System;
using System.Collections.Generic;
using DTO.Global;

namespace DTO.UserControllerDTO
{
    public class UserDTO : UserWithOptionalProprities
    {
        public UserDTO()
        {
            Car = new HashSet<CarDTO>();
            Carpooling = new HashSet<CarpoolingDTO>();
            CarpoolingApplicant = new HashSet<CarpoolingApplicantDTO>();
            PrivateMessage = new HashSet<PrivateMessageDTO>();
        }
        public ICollection<CarDTO> Car { get; set; }
        public ICollection<CarpoolingDTO> Carpooling { get; set; }
        public ICollection<CarpoolingApplicantDTO> CarpoolingApplicant { get; set; }
        public ICollection<PrivateMessageDTO> PrivateMessage { get; set; }
        public ICollection<TrustedCarpoolingDriverDTO> TrustedCarpoolingDriverUserNavigation { get; set; }
    }
}
