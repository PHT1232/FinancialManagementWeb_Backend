using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DbEntities.Pictures
{
    public class UserProfilePicture
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
        public DateTime Updated { get; set; }
    }
}
