using System;
using System.ComponentModel.DataAnnotations;


namespace DTO.Global {
    public abstract class CarpoolingGlobal {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "number of places is missing")]
        [Range(1,7)]
        public int NbPlaces { get; set; }
        [Required(ErrorMessage = "place price is missing")]
        [Range(0, 1000, ErrorMessage = "plce price must be between 0 and 1000")]
        public decimal PlacePrice { get; set; }
        [Required(ErrorMessage = "car id is missing")]
        public int Car { get; set; }
        [Required(ErrorMessage = "carpooling id is missing")]
        public string Creator { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required(ErrorMessage = "the starting date is missing")]
        public DateTime DateStart { get; set; }
        [Required(ErrorMessage = "date of the end is missing")]
        public DateTime DateEnd { get; set; }
        [Required(ErrorMessage = "address is missing")]
        [RegularExpression(@"^(\d+ ?,? ?)?([A-z', \d])+ ?(\d+ (boite ?\d))?$", ErrorMessage = "address is not correctly write")]
        public string DestinationFrom { get; set; }
        [Required(ErrorMessage = "address is missing")]
        [RegularExpression(@"^(\d+ ?,? ?)?([A-z', \d])+ ?(\d+ (boite ?\d))?$", ErrorMessage = "address is not correctly write")]
        public string DestinationTo { get; set; }
        [Required(ErrorMessage = "locality is missing")]
        [RegularExpression(@"^[A-z,' -]+$", ErrorMessage = "locality must contains no number")]
        public string LocalityFrom { get; set; }
        [Required(ErrorMessage = "locality is missing")]
        [RegularExpression(@"^[A-z,' -]+$", ErrorMessage = "locality must contains no number")]
        public string LocalityTo { get; set; }
        [Required(ErrorMessage = "postalCode is missing")]
        [RegularExpression(@"^\d{4,5}$", ErrorMessage = "code postal must be four or five numbers")]
        public string PostalCodeTo { get; set; }
        [Required(ErrorMessage = "postalCode is missing")]
        [RegularExpression(@"^\d{4,5}$", ErrorMessage = "code postal must be four or five numbers")]
        public string PostalCodeFrom { get; set; }
        public byte[] Timestamp { get; set; }
    }
}