using System;
using System.Collections.Generic;

namespace model
{
    public partial class TrustedCarpoolingDriver
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Carpooler { get; set; }
        public DateTime? CreatedAt { get; set; }
        
        public User CarpoolerNavigation { get; set; }
        public User UserNavigation { get; set; }
    }
}
