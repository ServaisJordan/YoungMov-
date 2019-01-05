using System;

namespace DTO.Global {
    public abstract class CarpoolingApplicantGlobal {
        public int Id { get; set; }
        public int User { get; set; }
        public int Carpooling { get; set; }
        public bool HasBeenAccepted { get; set; }
    }
}