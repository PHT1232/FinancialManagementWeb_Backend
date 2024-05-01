using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.GroupModels
{
    public class AddUserModel
    {
        public string GroupId { get; set; }
        public string UserId { get; set; }
        public string UserAdded {  get; set; }
        public string Role { get; set; }
    }
}
