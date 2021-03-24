using CursosAPI.Models.Usuario;
using CursosAPI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CursosAPI.Services
{
    public class JwtService : IAuthenticationService
    {
        private readonly IConfiguration configuration;
        public JwtService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput)
        {
            var secret = Encoding.ASCII.GetBytes(configuration.GetSection("JwtConfiguration:Secret").Value);
            var symmetricSecurityToken = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.id.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.email.ToString()),

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityToken, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);
            return token;
        }
    }
}
