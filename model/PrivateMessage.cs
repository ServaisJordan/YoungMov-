using System;
using System.Collections.Generic;

namespace model
{
    public partial class PrivateMessage
    {
        public PrivateMessage()
        {
            InverseReponseNavigation = new HashSet<PrivateMessage>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public bool HasBeenRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Creator { get; set; }
        public int? Reponse { get; set; }

        public User CreatorNavigation { get; set; }
        public PrivateMessage ReponseNavigation { get; set; }
        public ICollection<PrivateMessage> InverseReponseNavigation { get; set; }
    }
}
