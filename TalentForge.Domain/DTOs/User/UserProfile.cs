namespace TalentForge.Domain.DTOs.User;

public class UserProfile
{
    public string Profession { get; set; } = null!;
    public string ProfessionArea { get; set; } = null!;
    public string? SkillLevel { get; set; }
}