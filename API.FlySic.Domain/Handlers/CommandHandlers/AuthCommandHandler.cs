using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Interfaces.UnitOfWork;
using API.FlySic.Domain.Models.Response;
using API.FlySic.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Handlers.CommandHandlers
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponseModel>
    {
        private readonly INotificationService _notifications;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthCommandHandler(INotificationService notifications, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _notifications = notifications;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<AuthResponseModel> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user is null || !user.VerifyPassword(request.Password))
            {
                _notifications.AddNotification("Handle", "Invalid email or password.");
                return new AuthResponseModel();
            }

            var token = GenerateJwtToken(user);

            return new AuthResponseModel
            {
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("O'k.o'9*KlJ7SIe!W/P^9JWRX~bWj)N8jwMWPo5S"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.PhoneNumber, user.Phone),
                new Claim("IsDonateHours", user.IsDonateHours.ToString().ToLower()),
                new Claim("IsSearchHours", user.IsSearchHours.ToString().ToLower()),
                new Claim("IsFirstAccess", user.IsFirstAccess.ToString().ToLower())
            };

            var token = new JwtSecurityToken(
                issuer: "fly-sic",
                audience: "fly-sic",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
