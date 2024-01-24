using Microsoft.EntityFrameworkCore;
using TalentForge.Domain.Entities;

namespace TalentForge.Application.Interfaces;

public interface IDataContext
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<RequestToProject> RequestToProjects { get; set; }
    DbSet<UntestedUser> UntestedUsers { get; set; }
}