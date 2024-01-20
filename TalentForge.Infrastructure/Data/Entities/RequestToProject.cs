namespace TalentForge.Infrastructure.Data.Entities;

public class RequestToProject
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MessageId { get; set; }
    public string Content { get; set; } = null!;
}