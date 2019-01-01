using System;
using System.Collections.Generic;

namespace DTO
{
    public partial class CarDTO
    {
        public CarDTO()
        {
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ValidatedAt { get; set; }
        public string Color { get; set; }
        public string LicensePlateNumber { get; set; }
        public string CarModel { get; set; }

    }
}
