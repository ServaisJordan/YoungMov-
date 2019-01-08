using System;
using System.ComponentModel.DataAnnotations;


namespace DTO.Global {
    public class CarGlobal {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ValidatedAt { get; set; }
        [Required(ErrorMessage = "color is missing")]
        [RegularExpression(@"^[A-z]+$", ErrorMessage = "color contains invalid character")]
        public string Color { get; set; }
        [Required(ErrorMessage = "license plate number is missing")]
        [RegularExpression("^([0-9]{1,4}[A-Z]{1,4}[0-9]{1,2})|([A-Z]{1,2}[0-9]{1,3}[A-Z]{1,2})$", ErrorMessage = "invalid plate number")]
        public string LicensePlateNumber { get; set; }
        [Required(ErrorMessage = "car model is missing")]
        [RegularExpression(@"^[A-z\-]+$", ErrorMessage = "invalid carModel (number are not allowed)")]
        public string CarModel { get; set; }
        [Required(ErrorMessage = "owner id is missing")]
        public string Owner { get; set; }
    }
}