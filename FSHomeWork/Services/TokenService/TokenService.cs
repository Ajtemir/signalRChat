using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FSHomeWork.DAL.Contexts;
using FSHomeWork.Helpers.Constants;
using FSHomeWork.Helpers.Extensions;
using FSHomeWork.Models.Dto;
using FSHomeWork.Models.Entities;
using FSHomeWork.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FSHomeWork.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public TokenService(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public TokenDto GetToken(UserAuthViewModel userAuthViewModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.Equals(userAuthViewModel.Email));
            if (user == null) return new TokenDto {IsSuccess = false, Message = "Пользователь не найден"};
            if (!user.CheckPassword(userAuthViewModel.Password))
                return new TokenDto {IsSuccess = false, Message = "Пароль не верен"};
            var accessToken = GenerateAccessToken(user);
            return new TokenDto { IsSuccess = true, Token = accessToken};
        }

        private string GenerateAccessToken(User user)
        {
            var jwt = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                notBefore: DateTime.UtcNow,
                claims: new []
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    
                },
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(TokenConstants.AccessTokenLifeTime)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"])), SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        
    }
}