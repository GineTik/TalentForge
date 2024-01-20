namespace TalentForge.Domain.DTOs;

public class RequestToProject
{
    public int UserId { get; set; }
    public int MessageId { get; set; }
    public string Content { get; set; } = null!;
}