using System;
using System.Collections.Generic;

namespace DTO
{
    public partial class PrivateMessageDTO
    {
        public PrivateMessageDTO()
        {
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public bool HasBeenRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? Reponse { get; set; }

        public PrivateMessageDTO ReponseNavigation { get; set; }
    }
}
