using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Accounts
{
    public class TokenDomain
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public bool Success { get; set; }

        public string Errors { get; set; }

        public string Role { get; set; }
    }
}
