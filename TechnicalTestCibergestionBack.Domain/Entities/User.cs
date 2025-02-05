namespace TechnicalTestCibergestionBack.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Voter"; // "Admin" o "Voter"
        public bool IsLocked { get; set; } = false; // Para bloquear después de 3 intentos
        public int FailedAttempts { get; set; } = 0;
    }
}
