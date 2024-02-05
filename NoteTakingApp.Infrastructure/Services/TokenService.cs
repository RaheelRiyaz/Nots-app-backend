using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NoteTakingApp.Application.Abstractions.IServices;
using NoteTakingApp.Domain.Entities;
using NoteTakingApp.Infrastructure.AppClaims;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string GnerateToken(User user)
        {
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(AppClaim.Id, user.Id.ToString()),
                    new Claim(AppClaim.Email, user.Email),

                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)), SecurityAlgorithms.HmacSha256),
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                Expires = DateTime.Now.AddDays(1)
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
