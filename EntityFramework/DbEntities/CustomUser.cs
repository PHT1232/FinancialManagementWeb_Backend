using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DbEntities
{
    public class CustomUser : IdentityUser
    {
        public string UserRealName { get; set; }
    }
}
