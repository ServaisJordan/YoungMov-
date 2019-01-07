using System;
using System.Collections.Generic;

namespace model
{
    public partial class CarpoolingApplicant
    {
        public int Id { get; set; }
        public int Carpooling { get; set; }
        public string User { get; set; }

        public Carpooling CarpoolingNavigation { get; set; }
        public User UserNavigation { get; set; }
    }
}
