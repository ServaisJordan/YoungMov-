using System;
using System.ComponentModel.DataAnnotations;


namespace DTO.Global {
    public abstract class CarpoolingApplicantGlobal {
        public int Id { get; set; }
        [Required(ErrorMessage = "user id is missing")]
        public string User { get; set; }
        [Required(ErrorMessage = "carpooling id is missing")]
        public int Carpooling { get; set; }
    }
}