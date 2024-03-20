using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.AuthModel
{
    public class LoginInfo
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime Exp {  get; set; }
        public string Role { get; set; }
    }
}
