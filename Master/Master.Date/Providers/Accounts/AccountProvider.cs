using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using Master.Data.Models.Account;
using Master.Data.Models.Role;
using Master.Domain.Accounts;
using Master.Domain.Authorization;
using Master.Shared;
using Master.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using static Master.Domain.Authorization.Permissions;

namespace Master.Data.Providers.Accounts
{
    internal class AccountProvider : GenericProvider<UserAccount>, IAccountProvider
    {

        private readonly UserManager<UserAccount> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        //private readonly ILoggerProvider _logger;

        public AccountProvider(MasterDbContext dbContext,
            UserManager<UserAccount> userManager,
            RoleManager<UserRole> roleManager,
            IMapper mapper,
            //NLog.ILogger logger,
            IStringLocalizerFactory factory)
        {
            DbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
            //_logger = logger;
            _roleManager = roleManager;

            _localizer = factory.Create(typeof(CommonResource));
        }

        public async Task<UserAccountDomain> LoginAsync(string username, string password)
        {
            var userEntity = await ActiveDbSet.Include(x=>x.UserRoles).Where(u => u.UserName == username).FirstOrDefaultAsync();

            if (userEntity == null) throw new EntityNotFoundException(nameof(UserAccount), username);

            var result = CheckPassword(password, userEntity.PasswordHash, out string newPasswordHash);

            if (result == PasswordVerificationResult.Failed)
            {
               
            }
            else if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                userEntity.PasswordHash = newPasswordHash;
                await DbContext.SaveChangesAsync();
            }

            return new UserAccountDomain()
            {
                Id = userEntity.Id,
                Email = userEntity.Email.ToLower(),
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                UserName = userEntity.UserName,
                Role = userEntity.UserRoles.FirstOrDefault()?.Name
            };

        }

        public async Task<RegistrationResult> RegisterAsync(RegisterInputCommand command)
        {
            var userRole = await _roleManager.Roles.FirstAsync(x => x.Name == StaticRoleNames.Passenger);

            var userAccount = new UserAccount
            {
                UserName = command.Email,
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber,
                PhoneNumberConfirmed = true,
                IsActive = true,
                IsCodeConfirmed = false,
                UserRoles = new List<UserRole>()
            };

            userAccount.UserRoles.Add(userRole);

            // userAccount.ConfirmationCode = await _userManager.GenerateTwoFactorTokenAsync(userAccount, TokenProvider);

            var identityResult = await _userManager.CreateAsync(userAccount, command.Password);

            var result = new RegistrationResult(identityResult.Succeeded);

            if (!result.Succeeded)
            {
                result.Errors.AddRange(identityResult.Errors.Select(x => x.Description));
                //_logger.Error(@$"Unsuccessful attempt to register new patient user ( {command.Email}).
                //     Errors: {string.Join(", ", result.Errors)}");
            }

            result.Id = userAccount.Id;

            return result;
        }

        public async Task<UserAccountDomain> FindUserAsync(string email)
        {           
            var user = await ActiveDbSet.Include(x=>x.UserRoles).FirstOrDefaultAsync(x => x.Email == email && x.IsDeleted != true);

            return _mapper.Map<UserAccountDomain>(user);
        }

        public static string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(null, password);
        }

        public static string HashPasswordV1(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return password;
            }
            using (var sha256 = SHA256.Create())
            {
                // Send a text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static PasswordVerificationResult CheckPassword(string password, string hash, out string newHash)
        {
            var passwordHasher = new PasswordHasher<object>();

            var result = passwordHasher.VerifyHashedPassword(null, hash, password);

            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                newHash = HashPassword(password);
                return result;
            }

            if (result == PasswordVerificationResult.Success)
            {
                newHash = hash;
                return result;
            }

            var oldHash = HashPasswordV1(password);

            if (oldHash == hash)
            {
                newHash = HashPassword(password);
                return PasswordVerificationResult.SuccessRehashNeeded;
            }
            newHash = null;
            return PasswordVerificationResult.Failed;
        }

        public static bool CheckPasswordPolicy(string password, PasswordOptions opts = null)
        {
            if (opts == null)
                opts = new PasswordOptions();
            if (password.Length < opts.RequiredLength)
                return false;
            int d = 0, l = 0, u = 0, s = 0;
            var set = new HashSet<char>();
            foreach (var x in password)
            {
                if (char.IsDigit(x))
                    d++;
                else if (char.IsUpper(x))
                    u++;
                else if (char.IsLower(x))
                    l++;
                else
                    s++;
                set.Add(x);
            }
            return set.Count >= opts.RequiredUniqueChars
                && (!opts.RequireDigit || d > 0)
                && (!opts.RequireLowercase || l > 0)
                && (!opts.RequireUppercase || u > 0)
                && (!opts.RequireNonAlphanumeric || s > 0);
        }

        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",
                "abcdefghijkmnopqrstuvwxyz",
                "0123456789",
                "!@$?_-"
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
