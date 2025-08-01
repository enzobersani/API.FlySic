﻿using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Interfaces.Services;
using API.FlySic.Domain.Models;
using API.FlySic.Domain.Notifications;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

namespace API.FlySic.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly INotificationService _notification;
        private readonly EmailSettings _settings;

        public EmailService(IConfiguration configuration, INotificationService notification, IOptions<EmailSettings> settings)
        {
            _configuration = configuration;
            _notification = notification;
            _settings = settings.Value;
        }

        public async Task SendNewUserEmailAsync(string toEmail, NewUserCommand form)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse("egbersani@gmail.com"));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = "🟢 Novo usuário cadastrado no FlySic";

                var builder = new BodyBuilder();

                // Corpo em HTML
                builder.HtmlBody = $@"
                        <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                            <h2 style='color: #f69752;'>📩 Novo cadastro recebido no FlySic</h2>
                            <p>Segue abaixo os dados do novo usuário:</p>
                            <table style='border-collapse: collapse; width: 100%; margin-top: 10px;'>
                                <tr><td style='padding: 8px; font-weight: bold;'>Nome:</td><td>{form.Name}</td></tr>
                                <tr><td style='padding: 8px; font-weight: bold;'>Email:</td><td>{form.Email}</td></tr>
                                <tr><td style='padding: 8px; font-weight: bold;'>CPF:</td><td>{form.Cpf}</td></tr>
                                <tr><td style='padding: 8px; font-weight: bold;'>Telefone:</td><td>{form.Phone}</td></tr>
                                <tr><td style='padding: 8px; font-weight: bold;'>Data de Nascimento:</td><td>{form.BirthDate}</td></tr>
                                <tr><td style='padding: 8px; font-weight: bold;'>Aceitou os Termos:</td><td>{(form.IsAcceptedTerms ? "Sim" : "Não")}</td></tr>
                                <tr><td style='padding: 8px; font-weight: bold;'>É doador de horas?:</td><td>{(form.IsDonateHours ? "Sim" : "Não")}</td></tr>
                                <tr><td style='padding: 8px; font-weight: bold;'>Esta buscando horas?:</td><td>{(form.IsSearchHours ? "Sim" : "Não")}</td></tr>
                            </table>
                            <p style='margin-top: 20px;'>Por favor, revise e tome as ações necessárias.</p>
                            <p>📎 Documento enviado está em anexo (se aplicável).</p>
                            <p style='color: gray; font-size: 12px;'>FlySic - Sistema de Cadastro de Usuários</p>
                        </div>";

                // Corpo em texto (fallback)
                builder.TextBody = $@"Novo usuário cadastrado:
                                    - Nome: {form.Name}
                                    - Email: {form.Email}
                                    - CPF: {form.Cpf}
                                    - Telefone: {form.Phone}
                                    - Data de Nascimento: {form.BirthDate}
                                    - Aceitou Termos: {(form.IsAcceptedTerms ? "Sim" : "Não")}";

                // Adiciona anexo, se houver
                if (form.Document != null && form.Document.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await form.Document.CopyToAsync(memoryStream);
                    builder.Attachments.Add(form.Document.FileName, memoryStream.ToArray());
                }

                message.Body = builder.ToMessageBody();

                using var client = new MailKit.Net.Smtp.SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("egbersani@gmail.com", "rbfp syqw ejkq dfoi");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _notification.AddNotification("SendNewUserEmailAsync", ex.ToString());
            }
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody, string textBody = "", IFormFile? attachment = null)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = htmlBody,
                    TextBody = textBody
                };

                if (attachment != null && attachment.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await attachment.CopyToAsync(memoryStream);
                    builder.Attachments.Add(attachment.FileName, memoryStream.ToArray());
                }

                message.Body = builder.ToMessageBody();

                using var client = new MailKit.Net.Smtp.SmtpClient();
                await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_settings.SmtpUser, _settings.SmtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _notification.AddNotification("SendEmailAsync", ex.ToString());
            }
        }
    }
}
