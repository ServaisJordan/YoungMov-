using System;
using System.Collections.Generic;

namespace DTO
{
    public partial class TrustedCarpoolingDriverDTO
    {
        public int User { get; set; }
        public int Carpooler { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Timestamp { get; set; }

        public UserDTO CarpoolerNavigation { get; set; }
    }
}
