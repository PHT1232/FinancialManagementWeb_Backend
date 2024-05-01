using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.AuthModel
{
    public class ApplicationUser
    {
        public string? Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? UserProfile { get; set; }
        public string? Role { get; set; }
    }
}
