using System.ComponentModel.DataAnnotations.Schema;

namespace TalentForge.Domain.Entities;

public class UntestedUser
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OnTest { get; set; }
    
    [ForeignKey("Users")]
    public int? TestingByManagerId { get; set; }
}