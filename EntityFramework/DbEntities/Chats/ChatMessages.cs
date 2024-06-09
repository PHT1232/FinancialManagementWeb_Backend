﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DbEntities.Chats
{
    public class ChatMessages
    {
        public long Id { get; set; }
        public long ChatSessionId { get; set; }
        public string ChatMessage { get; set; }
        public List<string> Pictures { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
