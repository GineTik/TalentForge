namespace TalentForge.Infrastructure.Data.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<int> UserId { get; set; } = null!;
}