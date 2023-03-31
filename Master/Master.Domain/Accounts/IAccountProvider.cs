using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Accounts
{
    public interface IAccountProvider
    {
        Task<UserAccountDomain> LoginAsync(string username, string password);
    }
}
