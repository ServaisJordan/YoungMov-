using System;
using System.Collections.Generic;

namespace DTO.Global
{
    public class TrustedCarpoolingDriverGlobal
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Carpooler { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
