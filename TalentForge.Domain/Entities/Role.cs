namespace TalentForge.Domain.Entities;

public enum ExistingRoles : int
{
    Untested = 1,
    User = 2,
    Manager = 3,
    Administrator = 4,
    Owner = 5
}

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<User> Users { get; set; } = null!;
}