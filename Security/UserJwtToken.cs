using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Security
{
    public class UserJwtToken
    {
        private static readonly string mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
        private static readonly SymmetricSecurityKey mySecurityKey = new(Encoding.ASCII.GetBytes(mySecret));

        public static string Issuer { get; set; } = "http://mysite.com";
        public static string Audience { get; set; } = "http://myaudience.com";
        public bool IsValid { get { return JwtToken != null && DateTime.Now < Expires; } }
        public string UserId { get => GetClaim("nameid"); }
        public string Username { get => GetClaim("unique_name"); }
        public string Email { get => GetClaim("email"); }
        public string Role { get => GetClaim("role"); }
        public DateTime Expires { get => DateTimeOffset.FromUnixTimeSeconds(int.Parse(GetClaim("exp"))).LocalDateTime; }
        public string Token { get => token; }
        private string token;
        public JwtSecurityToken JwtToken { get => jwtToken; }
        private JwtSecurityToken jwtToken;

        public UserJwtToken(string token)
        {
            SetToken(token);
        }

        public IEnumerable<Claim> GetClaims()
        {
            //TODO! Make sure that we are valid
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            return securityToken.Claims;
        }

        public string GetClaim(string claimType)
        {
            //TODO! Make sure that we are valid
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            return securityToken.Claims.First(claim => claim.Type == claimType).Value;
        }

        private bool SetToken(string token)
        {
            this.token = token;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);

                jwtToken = validatedToken as JwtSecurityToken;
            }
            catch
            {
                jwtToken = null;
                return false;
            }

            return true;
        }

        //TODO! Handle array of roles
        public static string GenerateToken(int id, string email, string username, string role, string issuer = "http://mysite.com", string audience = "http://myaudience.com", int validMinutes = 1)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(validMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
