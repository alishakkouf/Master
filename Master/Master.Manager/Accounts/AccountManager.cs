using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.Accounts;
using Microsoft.Extensions.Configuration;

namespace Master.Manager.Accounts
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountProvider _accountProvider;
        private IConfiguration Configuration { get; set; }

        public AccountManager(IAccountProvider accountProvider, IConfiguration Configuration)
        {
            _accountProvider = accountProvider;
            this.Configuration = Configuration;
        }

        public async Task<TokenDomain> LoginAsync(string username, string password)
        {
            var user = await _accountProvider.LoginAsync(username, password);

            var token = JWTGenerator.GenerateJWTToken(new CreateTokenRequest { }, Configuration);

            return token;
        }
    }
}
