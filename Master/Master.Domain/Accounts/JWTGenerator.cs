using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Master.Domain.Accounts
{
    public class JWTGenerator
    {
        public static string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static TokenDomain GenerateJWTToken(CreateTokenRequest userInfo, IConfiguration appSettings)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                new Claim("IsActive"  , userInfo.IsActive.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            for (int i = 0; i < (userInfo?.Role?.Count ?? 0); i++)
            {
                var claim = new Claim("role", userInfo.Role[i]);
                claims.Add(claim);
            }
            var token = new JwtSecurityToken(
                issuer: appSettings["Jwt:Issuer"],
                audience: appSettings["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );
            string tok = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenDomain
            {
                AccessToken = tok, //"Bearer " + token,
                ExpiresIn = DateTime.Now.AddDays(1).Subtract(DateTime.Now).Seconds,
            };
        }


    }
}
