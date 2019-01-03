using System;

namespace DTO
{
    public abstract class UserGlobal
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Adresse { get; set; }
        public string Phone { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
    }
}