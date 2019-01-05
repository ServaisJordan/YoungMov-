using System;
using System.Collections.Generic;

namespace DTO.Global
{
    public class TrustedCarpoolingDriverGlobal
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Carpooler { get; set; }
        public DateTime? CreatedAt { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
