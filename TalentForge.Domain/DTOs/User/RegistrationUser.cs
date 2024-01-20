namespace TalentForge.Domain.DTOs.User;

public class RegistrationUser
{
    public long TelegramUserId { get; set; }
    public string Skills { get; set; } = null!;
    public ICollection<string> Portfolios { get; set; } = new List<string>();
}