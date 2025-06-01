using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Entities.Base;
using API.FlySic.Domain.Enum;
using Mapster;
using System.Security.Cryptography;
using System.Text;

namespace API.FlySic.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public bool IsAcceptedTerms { get; private set; }
        public bool IsDonateHours { get; private set; }
        public bool IsSearchHours { get; private set; }
        public UserStatusEnum Status { get; private set; }
        public string Password { get; private set; }
        private User(){}
        public static User New(NewUserCommand request)
        {
            var user = new User();
            request.Adapt(user);
            user.BirthDate = request.BirthDate.ToUniversalTime();
            user.GenerateAutomaticPassword();
            return user;
        }

        private void GenerateAutomaticPassword()
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";

            var random = new Random();
            var chars = new char[12];

            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            var password = new string(chars);
            Password = HashPassword(password);
        }

        public void UpdatePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("A senha não pode ser vazia");

            Password = HashPassword(newPassword);
        }

        public bool VerifyPassword(string password) => Password == HashPassword(password);

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var test =  Convert.ToBase64String(hashedBytes);
            return test;
        }
    }
}
