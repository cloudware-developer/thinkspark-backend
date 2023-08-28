using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThinkSpark.Shared.Security.Token.Interface;

namespace ThinkSpark.Shared.Security.Token
{
    public class TokenApplication : ITokenApplication
    {
        private readonly IConfiguration _configuration;

        public TokenApplication(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CriarToken(Claim[] claims)
        {
            try
            {
                var secretKey = _configuration.GetSection("JwtConfig").GetValue<string>("Secret");
                var daysTokenExpiration = _configuration.GetSection("JwtConfig").GetValue<int>("DaysTokenExpiration");
                var symmetricSecurityKey = Encoding.ASCII.GetBytes(secretKey);
                var securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(daysTokenExpiration),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricSecurityKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var securityTokenHandler = new JwtSecurityTokenHandler();
                var securityKey = securityTokenHandler.CreateToken(securityTokenDescriptor);
                var token = securityTokenHandler.WriteToken(securityKey);
                return token;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao gerar token de acesso. {ex.Message}");
            }
        }

        public string CriarTokenWithExpireTime(Claim[] claims, int expiresDays)
        {
            try
            {
                var secretKey = _configuration.GetSection("JwtConfig").GetValue<string>("Secret");
                var daysTokenExpiration = expiresDays > 0 ? expiresDays : _configuration.GetSection("JwtConfig").GetValue<int>("DaysTokenExpiration");
                var symmetricSecurityKey = Encoding.ASCII.GetBytes(secretKey);
                var securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(daysTokenExpiration),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricSecurityKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var securityTokenHandler = new JwtSecurityTokenHandler();
                var securityKey = securityTokenHandler.CreateToken(securityTokenDescriptor);
                var token = securityTokenHandler.WriteToken(securityKey);
                return token;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao gerar token de acesso. {ex.Message}");
            }
        }

        public List<Claim> DescriptografarToken(string token) 
        {
            var secretKey = _configuration.GetSection("JwtConfig").GetValue<string>("Secret");
            var tokenHandler = new JwtSecurityTokenHandler();
            //var symmetricSecurityKey = Convert.FromBase64String(secretKey);
            var symmetricSecurityKey = Encoding.ASCII.GetBytes(secretKey);
            var parametros = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(symmetricSecurityKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, parametros, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            var claims = new List<Claim>();

            if (jwtSecurityToken != null && jwtSecurityToken.Claims.Any())
                claims = jwtSecurityToken.Claims.ToList();

            return claims;
        }

    }
}
