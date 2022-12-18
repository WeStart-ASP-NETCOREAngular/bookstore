using BookStore.API.DTOs;
using BookStore.API.Interfaces;
using BookStore.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.API.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public JwtTokenGenerator(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<AuthenticationResponse> Generate(AppUser user)
        {
            // Claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim("Email" , user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            

            //SecretKey
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secert"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var Expiration = DateTime.Now.AddMonths(1);


            // token 
            var token = new JwtSecurityToken(
               issuer: _configuration["JWT:ValidIssuer"],
               audience: _configuration["JWT:ValidAudience"],
               claims: claims,
               expires: Expiration,
               signingCredentials: cred);

            return new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = Expiration
            };
        }
    }
}
