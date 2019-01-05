using System;

namespace DTO.Global {
    public class CarGlobal {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ValidatedAt { get; set; }
        public string Color { get; set; }
        public string LicensePlateNumber { get; set; }
        public string CarModel { get; set; }
        public int Owner { get; set; }
    }
}