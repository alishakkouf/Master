using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Master.Data.Models;
using Master.Domain.Accounts;
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
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _localizer;

        public AccountProvider(MasterDbContext dbContext,
            IMapper mapper,
            IStringLocalizerFactory factory)
        {
            DbContext = dbContext;
            _mapper = mapper;
            _localizer = factory.Create(typeof(CommonResource));
        }

        public async Task<UserAccountDomain> LoginAsync(string username, string password)
        {
            var userEntity = await ActiveDbSet.Where(u => u.UserName == username).FirstOrDefaultAsync();

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
