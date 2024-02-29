﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DbEntities.Groups
{
    public class GroupUsers
    {
        public int Id { get; set; }
        public string GroupId { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserAdded { get; set; }
    }
}
