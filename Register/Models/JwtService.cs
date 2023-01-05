﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Register.Models
{
    public class JwtService
    {
        public String Secretkey { get; set; }

        public int TokenDuration { get; set; }

        private readonly IConfiguration config;

        public JwtService(IConfiguration _config) 
        { 
            config = _config;
            this.Secretkey = config.GetSection("jwtConfig").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);
        }

        public String GenerateToken(String id, String firstname, String lastname, String email, String mobile, String gender)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Secretkey));

            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new Claim("id", id),
                new Claim("firstname", firstname),
                new Claim("lastname", lastname),
                new Claim("email", email),
                new Claim("mobile", mobile),
                new Claim("gender", gender)
            };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials: signature
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);

        }
    }
}
