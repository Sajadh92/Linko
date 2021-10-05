using Linko.Domain;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Linko.Helper
{
    public static class JsonWebToken
    {
        public static string GenerateToken(UserManager UserManager)
        {
            byte[] symmetricKey = Convert.FromBase64String(Key.SecretKey);
            SymmetricSecurityKey securityKey = new(symmetricKey);
            string algorithms = SecurityAlgorithms.HmacSha256Signature;
            JwtSecurityTokenHandler tokenHandler = new();

            SecurityToken stoken = tokenHandler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Expires = Key.DateTimeIQ.AddDays(30),
                    Subject = new ClaimsIdentity(new[] {
                        new Claim(ClaimInfo.UserManager, JsonConvert.SerializeObject(UserManager))
                    }),
                    SigningCredentials = new SigningCredentials(securityKey, algorithms)
                });

            return tokenHandler.WriteToken(stoken);
        }
    }
}
