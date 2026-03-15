using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fragrance_API.jwt
{
    public class TokenGenerator
    {
        public string GenerateToken(int id,string username,string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
            
            if (jwtSecret == null) throw new ArgumentNullException(" Key is null,check for the environment-variable 'JWT_SECRET'. ");

            var key = Encoding.UTF8.GetBytes(jwtSecret);
            

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, id.ToString()),
                new(JwtRegisteredClaimNames.PreferredUsername, username),
                new(JwtRegisteredClaimNames.Email, email)

            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = "https://localhost:7014/api/Fragrance_Flow",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
