using CostApplication.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace CostApplication.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator    {
        
        private readonly AuthenticationConfiguration _configuration;       

        public JwtTokenGenerator(AuthenticationConfiguration configuration)
        {
            _configuration = configuration;
        }
     
        public string GenerateToken(User user) 
        {
            var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_configuration.ExpirationsMinutes),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }    
}
