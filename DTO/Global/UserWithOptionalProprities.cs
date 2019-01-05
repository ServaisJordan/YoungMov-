using System;
using System.Collections.Generic;

namespace DTO.Global
{
    public class UserWithOptionalProprities : UserGlobal
    {
        public UserWithOptionalProprities()
        {
        }
        public string FacePhotoFilename { get; set; }
        public DateTime? FacePhotoSentAt { get; set; }
        public DateTime? FacePhotoValidatedAt { get; set; }
        public string IdentityPieceFilename { get; set; }
        public DateTime? IdentityPieceSentAt { get; set; }
        public DateTime? IdentityPieceValidatedAt { get; set; }
        public DateTime? EmailValidatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string TrustedCarpoolingDriverCode { get; set; }
        public int Id { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
