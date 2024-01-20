using System.Security.Principal;

namespace TalentForge.Infrastructure.Data.Entities;

public class User
{
    public int Id { get; set; }
    public int TelegramUserId { get; set; }
    public string Profession { get; set; } = null!;
    public string ProfessionArea { get; set; } = null!;
    public string Portfolio { get; set; } = null!;
    public string? SkillLevel { get; set; }
    public bool OnTest { get; set; }
    public IEnumerable<int> RoleId { get; set; } = null!;
}