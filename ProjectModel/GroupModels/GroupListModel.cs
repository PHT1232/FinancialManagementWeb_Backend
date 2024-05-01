using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.GroupModels
{
    public class GroupListModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasPassword { get; set; }
        public string IconUrl { get; set; }
        public bool IsPublic { get; set; }
    }
}
