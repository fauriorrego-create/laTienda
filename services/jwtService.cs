using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using laTienda.Models;

namespace laTienda.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarToken(Usuario usuario, List<string> roles)
        {
            var keyString = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            // 🔴 VALIDACIÓN COMPLETA
            if (string.IsNullOrEmpty(keyString))
                throw new Exception("Jwt:Key no configurado");

            if (string.IsNullOrEmpty(issuer))
                throw new Exception("Jwt:Issuer no configurado");

            if (string.IsNullOrEmpty(audience))
                throw new Exception("Jwt:Audience no configurado");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Idusuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre ?? ""),
                new Claim(ClaimTypes.Email, usuario.Email ?? "")
            };

            if (roles != null)
            {
                foreach (var rol in roles)
                {
                    if (!string.IsNullOrEmpty(rol))
                        claims.Add(new Claim(ClaimTypes.Role, rol));
                }
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(keyString)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}