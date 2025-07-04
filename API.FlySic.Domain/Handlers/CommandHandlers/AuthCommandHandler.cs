using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Interfaces.Services;
using API.FlySic.Domain.Interfaces.UnitOfWork;
using API.FlySic.Domain.Models.Response;
using API.FlySic.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.FlySic.Domain.Handlers.CommandHandlers
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponseModel>,
                                      IRequestHandler<RecoveryCodeCommand, Unit>,
                                      IRequestHandler<ValidateRecoveryCodeCommand, bool>,
                                      IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly INotificationService _notifications;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthCommandHandler(INotificationService notifications, IUnitOfWork unitOfWork, IConfiguration configuration, IEmailService emailService)
        {
            _notifications = notifications;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _emailService = emailService;
        }

        #region Login
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
        #endregion

        #region Recovery Code
        public async Task<Unit> Handle(RecoveryCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user is null)
            {
                _notifications.AddNotification("Handle", "User not found.");
                return Unit.Value;
            }

            var code = new Random().Next(100000, 999999).ToString();
            await _unitOfWork.RecoveryCodeRepository.SaveCodeAsync(request.Email, code, TimeSpan.FromMinutes(10));

            var html = $@"
            <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                <h2 style='color: #f69752;'>🔐 Código de Recuperação de Senha</h2>
                <p>Olá <strong>{user.Name}</strong>,</p>
                <p>Seu código de recuperação de senha é:</p>
                <h3 style='background: #f4f4f4; padding: 10px; border-radius: 5px;'>{code}</h3>
                <p>Este código é válido por <strong>10 minutos</strong>.</p>
                <p>Se você não solicitou essa recuperação, ignore este e-mail.</p>
                <hr />
                <p style='color: gray; font-size: 12px;'>FlySIC - Sistema de Gerenciamento de Horas de Voo</p>
            </div>";

            var plainText = $"Olá {user.Name}, seu código de recuperação é: {code}. Ele expira em 10 minutos.";

            await _emailService.SendEmailAsync(
                user.Email,
                "🔐 Código de recuperação de senha - FlySIC",
                html,
                plainText
            );

            return Unit.Value;
        }
        #endregion

        #region Validate Recovery Code
        public async Task<bool> Handle(ValidateRecoveryCodeCommand request, CancellationToken cancellationToken)
        {
            var isValid = await _unitOfWork.RecoveryCodeRepository.ValidateCodeAsync(request.Email, request.Code);
            if (!isValid)
            {
                _notifications.AddNotification("Handle", "Invalid or expired recovery code.");
                return false;
            }

            return true;
        }
        #endregion

        #region Reset Password
        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user is null)
            {
                _notifications.AddNotification("Handle", "User not found.");
                return Unit.Value;
            }

            var isValid = await _unitOfWork.RecoveryCodeRepository.ValidateCodeAsync(request.Email, request.Code);
            if (!isValid)
            {
                _notifications.AddNotification("Handle", "Invalid or expired recovery code.");
                return Unit.Value;
            }

            user.UpdatePassword(request.NewPassword);
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.RecoveryCodeRepository.ClearCodeAsync(request.Email);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
        #endregion

        #region Private Methods
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
        #endregion
    }
}
