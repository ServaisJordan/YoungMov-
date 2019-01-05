using System;
using System.Collections.Generic;

namespace model
{
    public partial class User
    {
        public User()
        {
            Car = new HashSet<Car>();
            Carpooling = new HashSet<Carpooling>();
            CarpoolingApplicant = new HashSet<CarpoolingApplicant>();
            PrivateMessage = new HashSet<PrivateMessage>();
            TrustedCarpoolingDriverCarpoolerNavigation = new HashSet<TrustedCarpoolingDriver>();
            TrustedCarpoolingDriverUserNavigation = new HashSet<TrustedCarpoolingDriver>();
        }

        public int Id { get; set; }
        public string Password { get; set; }
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
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public byte[] Timestamp { get; set; }

        public ICollection<Car> Car { get; set; }
        public ICollection<Carpooling> Carpooling { get; set; }
        public ICollection<CarpoolingApplicant> CarpoolingApplicant { get; set; }
        public ICollection<PrivateMessage> PrivateMessage { get; set; }
        public ICollection<TrustedCarpoolingDriver> TrustedCarpoolingDriverCarpoolerNavigation { get; set; }
        public ICollection<TrustedCarpoolingDriver> TrustedCarpoolingDriverUserNavigation { get; set; }
    }
}
