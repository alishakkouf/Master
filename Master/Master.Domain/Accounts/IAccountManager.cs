using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Accounts
{
    public interface IAccountManager
    {
        Task<TokenDomain> LoginAsync(string username, string password);
    }
}
