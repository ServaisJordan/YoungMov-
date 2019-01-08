using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Global {
    public abstract class PrivateMessageGlobal {
        public int Id { get; set; }
        public string Content { get; set; }
        [Required(ErrorMessage = "id creator is missing")]
        public string Creator { get; set; }
        public int? Reponse { get; set; }
        public bool HasBeenRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}