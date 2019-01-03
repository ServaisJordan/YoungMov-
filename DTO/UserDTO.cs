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
        public string FacePhotoFilename { get; set; }
        public DateTime? FacePhotoSentAt { get; set; }
        public DateTime? FacePhotoValidatedAt { get; set; }
        public string IdentityPieceFilename { get; set; }
        public DateTime? IdentityPieceSentAt { get; set; }
        public DateTime? IdentityPieceValidatedAt { get; set; }
        public DateTime? EmailValidatedAt { get; set; }
        public ICollection<CarDTO> Car { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string TrustedCarpoolingDriverCode { get; set; }
        public int Id { get; set; }
        public byte[] Timestamp { get; set; }


        public ICollection<CarpoolingDTO> Carpooling { get; set; }
        //public ICollection<CarpoolingApplicantDTO> CarpoolingApplicant { get; set; }
        public ICollection<PrivateMessageDTO> PrivateMessage { get; set; }
        public ICollection<TrustedCarpoolingDriverDTO> TrustedCarpoolingDriverCarpoolerNavigation { get; set; }
        public ICollection<TrustedCarpoolingDriverDTO> TrustedCarpoolingDriverUserNavigation { get; set; }
    }
}
