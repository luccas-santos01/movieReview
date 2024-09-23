using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CineCritique.Models;
using Microsoft.IdentityModel.Tokens;

namespace CineCritique.Services
{
    public class TokenGenerator
    {
        public string Generate(User user)
        {
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new InvalidOperationException("JWT secret not configured.");
            var ExpiresHours = 4;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = AddClaims(user),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Expires = DateTime.UtcNow.AddHours(ExpiresHours)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity AddClaims(User user)
        {
            if (user.Email == null) throw new ArgumentNullException(nameof(user.Email), "Email cannot be null");
            if (user.Name == null) throw new ArgumentNullException(nameof(user.Name), "Name cannot be null");

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            claims.AddClaim(new Claim(ClaimTypes.Role, user.Admin.ToString() ?? "False"));
            return claims;
        }
    }
}