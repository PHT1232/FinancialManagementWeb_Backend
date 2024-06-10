using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DbEntities.Pictures
{
    public class ChatUserPicture
    {
        public long Id { get; set; }
        public long ChatMessagesId { get; set; }
        public string Url { get; set; }
    }
}
