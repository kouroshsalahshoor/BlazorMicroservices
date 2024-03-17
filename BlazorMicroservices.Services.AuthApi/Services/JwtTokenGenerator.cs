using BlazorMicroservices.Services.AuthApi.Models;
using BlazorMicroservices.Services.AuthApi.Services.IServices;
using BlazorMicroservices.Services.AuthApi.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorMicroservices.Services.AuthApi.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
        {
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = new List<Claim>
            {
                new Claim("Id",applicationUser.Id),
                new Claim("Email",applicationUser.Email),
                new Claim("UserName",applicationUser.UserName),
                new Claim("FirstName",applicationUser.FirstName),
                new Claim("LastName",applicationUser.LastName),
                new Claim("PhoneNumber",applicationUser.PhoneNumber),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}