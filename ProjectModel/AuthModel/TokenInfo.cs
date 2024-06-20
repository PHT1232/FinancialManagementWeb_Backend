using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModel.AuthModel
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime Exp {  get; set; }
    }
}
