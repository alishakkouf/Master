﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Accounts
{
    public class CreateTokenRequest
    {
        public long UserId { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public List<string> Role { get; set; }
    }
}
