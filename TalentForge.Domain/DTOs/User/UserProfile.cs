namespace TalentForge.Domain.DTOs.User;

public class UserProfile
{
    public int Id { get; set; }
    public string Skills { get; set; } = null!;
    public IEnumerable<string> Portfolios { get; set; } = null!;
    public string? SkillLevel { get; set; }
}