namespace TalentForge.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public int TelegramUserId { get; set; }
    public string Skills { get; set; } = null!;
    public string Portfolios { get; set; } = null!;
    public string? SkillLevel { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}