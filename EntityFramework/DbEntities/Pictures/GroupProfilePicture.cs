using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DbEntities.Pictures
{
    public class GroupProfilePicture
    {
        public int Id { get; set; }
        public string GroupId { get; set; }
        public string Url { get; set; }
        public string WhoUpdated { get; set; }
        public DateTime Updated { get; set; }
    }
}
