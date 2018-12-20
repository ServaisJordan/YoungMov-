using System;
using System.Collections.Generic;

namespace DTO
{
    public partial class UserDTO
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

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public DateTime? EmailValidatedAt { get; set; }
        public string Gender { get; set; }
        public string Adresse { get; set; }
        public string FacePhotoFilename { get; set; }
        public DateTime? FacePhotoSentAt { get; set; }
        public DateTime? FacePhotoValidatedAt { get; set; }
        public string IdentityPieceFilename { get; set; }
        public DateTime? IdentityPieceSentAt { get; set; }
        public DateTime? IdentityPieceValidatedAt { get; set; }
        public string Phone { get; set; }
        public string TrustedCarpoolingDriverCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public byte[] Timestamp { get; set; }

        public ICollection<CarDTO> Car { get; set; }
        public ICollection<CarpoolingDTO> Carpooling { get; set; }
        //public ICollection<CarpoolingApplicantDTO> CarpoolingApplicant { get; set; }
        public ICollection<PrivateMessageDTO> PrivateMessage { get; set; }
        public ICollection<TrustedCarpoolingDriverDTO> TrustedCarpoolingDriverCarpoolerNavigation { get; set; }
        public ICollection<TrustedCarpoolingDriverDTO> TrustedCarpoolingDriverUserNavigation { get; set; }
    }
}
