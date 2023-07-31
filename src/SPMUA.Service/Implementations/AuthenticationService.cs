using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SPMUA.Service.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SPMUA.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(int adminId)
        {
            string result;

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] securityKey = Encoding.ASCII.GetBytes(_configuration["AuthConfig:JwtSecurityKey"] ?? String.Empty);
            string validIssuer = _configuration["AuthConfig:JwtTokenValidIssuer"] ?? String.Empty;
            string validAudience = _configuration["AuthConfig:JwtTokenValidAudience"] ?? String.Empty;
            int tokenExpiresInDays = Convert.ToInt32(_configuration["AuthConfig:JwtTokenExpiresInDays"]);


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, adminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(tokenExpiresInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature),
                Issuer = validIssuer,
                Audience = validAudience
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            result = tokenHandler.WriteToken(token);

            return result;
        }
    }
}
