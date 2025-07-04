namespace API.FlySic.Domain.Entities
{
    public class RecoveryCode
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Email { get; private set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;
        public DateTime Expiration { get; private set; }

        public bool IsExpired() => DateTime.UtcNow > Expiration;

        public RecoveryCode(string email, string code, DateTime expiration)
        {
            Email = email;
            Code = code;
            Expiration = expiration;
        }
    }
}
