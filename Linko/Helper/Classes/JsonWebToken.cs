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
        public static string GenerateToken(UserJWTClaimDto userJWT)
        {
            byte[] symmetricKey = Convert.FromBase64String(Key.SecretKey);
            SymmetricSecurityKey securityKey = new(symmetricKey);
            string algorithms = SecurityAlgorithms.HmacSha256Signature;
            JwtSecurityTokenHandler tokenHandler = new();

            SecurityToken stoken = tokenHandler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Issuer = "Issuer",
                    Audience = "Audience",
                    NotBefore = Key.DateTimeIQ,
                    Expires = Key.DateTimeIQ.AddDays(30),
                    Subject = new ClaimsIdentity(new[] {
                        new Claim(Key.Lookup[Key.UserProfile], 
                        Key.GetHashString(JsonConvert.SerializeObject(userJWT)))
                    }),
                    SigningCredentials = new SigningCredentials(securityKey, algorithms)
                });

            return tokenHandler.WriteToken(stoken);
        }
    }
}
