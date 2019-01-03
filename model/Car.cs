using System;
using System.Collections.Generic;

namespace model
{
    public partial class Car
    {
        public Car()
        {
            Carpooling = new HashSet<Carpooling>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ValidatedAt { get; set; }
        public string Color { get; set; }
        public string LicensePlateNumber { get; set; }
        public string CarModel { get; set; }
        public int Owner { get; set; }

        public User OwnerNavigation { get; set; }
        public ICollection<Carpooling> Carpooling { get; set; }
    }
}
