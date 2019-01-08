using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO.Global
{
    public class TrustedCarpoolingDriverGlobal
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "id user is missing")]
        public string User { get; set; }
        [Required(ErrorMessage = "id carpooler is missing")]
        public string Carpooler { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
