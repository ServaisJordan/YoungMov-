using System;
using System.Collections.Generic;

namespace model
{
    public partial class CarpoolingApplicant
    {
        public int Id { get; set; }
        public int Carpooling { get; set; }
        public int User { get; set; }
        public bool HasBeenAccepted { get; set; }

        public Carpooling CarpoolingNavigation { get; set; }
        public User UserNavigation { get; set; }
    }
}
