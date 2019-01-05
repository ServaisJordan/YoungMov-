using System;

namespace DTO.Global {
    public abstract class PrivateMessageGlobal {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Creator { get; set; }
        public int? Reponse { get; set; }
        public bool HasBeenRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}