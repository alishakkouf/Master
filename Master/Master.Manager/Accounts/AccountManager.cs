using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.Accounts;
using Master.Shared;
using Master.Shared.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using static Master.Domain.Authorization.Permissions;

namespace Master.Manager.Accounts
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountProvider _accountProvider;
        private readonly IStringLocalizer _localizer;
        private IConfiguration Configuration { get; set; }

        public AccountManager(IAccountProvider accountProvider, IConfiguration Configuration,
             IStringLocalizerFactory factory)
        {
            _localizer = factory.Create(typeof(CommonResource));
            _accountProvider = accountProvider;
            this.Configuration = Configuration;
        }

        public async Task<TokenDomain> LoginAsync(string username, string password)
        {
            var user = await _accountProvider.LoginAsync(username, password);

            var token = JWTGenerator.GenerateJWTToken(new CreateTokenRequest 
            {
                Email= user.Email,
                Role =new List<string> { user.Role },
                IsActive = true,
                UserId = user.Id
            }, Configuration);

            return token;
        }

        public async Task<RegistrationResult> RegisterAsync(RegisterInputCommand command)
        {
            var existedUser = await _accountProvider.FindUserAsync(command.Email);

            if (existedUser != null) throw new BusinessException(_localizer["ExistedOnlineEmail"]);

            var result = await _accountProvider.RegisterAsync(command);

            if (!result.Succeeded)
                throw new BusinessException(_localizer["ErrorRegisteringNewPatient"]);

            return result;
        }
    }
}
