using System;
using System.ComponentModel.DataAnnotations;


namespace DTO.Global {
    public class CarGlobal {
        [Required(ErrorMessage = "id is required")]
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ValidatedAt { get; set; }
        [Required(ErrorMessage = "color is required")]
        [RegularExpression(@"^[A-z]+$", ErrorMessage = "color contains invalid character")]
        public string Color { get; set; }
        [Required(ErrorMessage = "license plate number is required")]
        [RegularExpression("^([0-9]{1,4}[A-Z]{1,4}[0-9]{1,2})|([A-Z]{1,2}[0-9]{1,3}[A-Z]{1,2})$", ErrorMessage = "invalid plate number")]
        public string LicensePlateNumber { get; set; }
        [Required(ErrorMessage = "car model is required")]
        [RegularExpression(@"^[A-z\-]+$")]
        public string CarModel { get; set; }
        public string Owner { get; set; }
    }
}