namespace TalentForge.Domain.DTOs.User;

public class RegistrationUser
{
    public int TelegramUserId { get; set; }
    public string Profession { get; set; } = null!;
    public string ProfessionArea { get; set; } = null!;
    public string SkillLevel { get; set; } = null!;
    public string Portfolio { get; set; } = null!;
}