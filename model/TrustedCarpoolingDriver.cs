using System;
using System.Collections.Generic;

namespace model
{
    public partial class TrustedCarpoolingDriver
    {
        public int User { get; set; }
        public int Carpooler { get; set; }
        public DateTime? CreatedAt { get; set; }
        public byte[] Timestamp { get; set; }

        public User CarpoolerNavigation { get; set; }
        public User UserNavigation { get; set; }
    }
}
