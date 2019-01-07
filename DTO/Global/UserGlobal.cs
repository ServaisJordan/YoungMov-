using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Global
{
    public abstract class UserGlobal
    {
        [Required(ErrorMessage = "user name is required")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "characters are not allowed")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "email is required")]
        [RegularExpression(@"^(([\w-\s]+)|([\w-]+(?:\.[\w-]+)*)|([\w-\s]+)([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)", ErrorMessage = "email is not conform")]
        public string Email { get; set; }
        [Required(ErrorMessage = "gender is required")]
        [RegularExpression(@"^[m|f|M|F]$", ErrorMessage = "gender must be man or woman (f, m)")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "address is required")]
        [RegularExpression(@"^(\d+ ?,? ?)?([A-z', \d])+ ?(\d+ (boite ?\d))?$", ErrorMessage = "address is not correctly write")]
        public string Address { get; set; }
        [RegularExpression(@"^0[1-68]([-. ]?\d{2}){4}$", ErrorMessage = "phone number the value don't match with a phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "locality is required")]
        [RegularExpression(@"^[A-z,' -]+$", ErrorMessage = "locality must contains no number")]
        public string Locality { get; set; }
        [Required(ErrorMessage = "postalCode is required")]
        [RegularExpression(@"^\d{4,5}$", ErrorMessage = "code postal must be four or five numbers")]
        public string PostalCode { get; set; }
    }
}

