using Fragrance_flow_DL_VERSION_.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fragrance_API.jwt
{
    public class TokenGenerator
    {
        /*public enum Roles
        {
            User = 0,
            Admin = 1,

        }*/
        public string GenerateToken(UserSession user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");

            if (jwtSecret == null) throw new ArgumentNullException(" Key is null,check for the environment-variable 'JWTSECRET'. ");

            var key = Encoding.UTF8.GetBytes(jwtSecret);
            var role = user.isAdmin  == 1 ? "Admin" : "User";

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Role,role),
                new Claim(ClaimTypes.Name,user.Username)

            };



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
