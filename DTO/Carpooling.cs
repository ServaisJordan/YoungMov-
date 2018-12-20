using System;
using System.Collections.Generic;

namespace DTO
{
    public partial class CarpoolingDTO
    {
        public CarpoolingDTO()
        {
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int NbPlaces { get; set; }
        public decimal PlacePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string DestinationFrom { get; set; }
        public string DestinationTo { get; set; }
        public string LocalityFrom { get; set; }
        public string LocalityTo { get; set; }
        public string PostalCodeTo { get; set; }
        public string PostalCodeFrom { get; set; }
        public int Car { get; set; }
        public int Creator { get; set; }
        public byte[] Timestamp { get; set; }

        public CarDTO CarNavigation { get; set; }
        public ICollection<CarpoolingApplicantDTO> CarpoolingApplicant { get; set; }
    }
}
