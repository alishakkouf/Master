using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Accounts
{
    public class RegistrationResult
    {
        public bool Succeeded { get; }

        public string Code { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public int Id { get; set; }

        public RegistrationResult(bool succeeded)
        {
            Succeeded = succeeded;
        }
    }
}
