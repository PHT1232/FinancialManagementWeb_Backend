using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.Chats
{
    public class Chat
    {
        public long Id { get; set; }
        public string UserSentId { get; set; }
        public string UserOrGroupReceivedId { get; set; }
        public string ChatMessage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set;}
    }
}
