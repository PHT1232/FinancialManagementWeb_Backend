using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DbEntities.Chats
{
    public class ChatModel
    {
        public string UserSentId { get; set; }
        public string UserOrGroupReceivedId { get; set; }
        public string ChatMessage { get; set; }
    }
}
