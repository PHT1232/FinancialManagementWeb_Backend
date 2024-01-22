﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.Groups
{
    public class Group
    {
        public string Id { get; set; }
        public string IconUrl { get; set; }
        public string Name { get; set; }
        public string password { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
    }
}
