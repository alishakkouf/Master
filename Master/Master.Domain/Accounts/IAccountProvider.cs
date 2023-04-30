﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Accounts
{
    public interface IAccountProvider
    {
        /// <summary>
        /// sign in the user.
        /// </summary>
        Task<UserAccountDomain> LoginAsync(string username, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<RegistrationResult> RegisterAsync(RegisterInputCommand command);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserAccountDomain> FindUserAsync(string email);
    }
}
