using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserExperienceAnalizer.API.UserExperienceAnalizer.Services
{
    public class TokenService
    {
        public string GetToken()
        {
            string secretKey = "@Rad_3838_API_UexAnalyser";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                 new Claim(ClaimTypes.Name, "RAD API"),
                 new Claim(ClaimTypes.Email, "viman.satharasinghe@gmail.com"),
            };

            var token = new JwtSecurityToken(
                issuer: "RADHOST",
                audience: "USER",
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials,
                claims: claims
            );

            string payload = token.EncodedHeader + " " + token.EncodedPayload;

            return payload;
        }
    }
}
