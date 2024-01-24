using Microsoft.EntityFrameworkCore;
using TalentForge.Application.Interfaces;
using TalentForge.Domain.Entities;

namespace TalentForge.Infrastructure.Data.EF;

public class DataContext : DbContext, IDataContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RequestToProject> RequestToProjects { get; set; }
    public DbSet<UntestedUser> UntestedUsers { get; set; }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public DataContext(DbContextOptions<DataContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .HasData(new Role
            {
                Id = (int)ExistingRoles.Untested,
                Name = "Untested"
            }, new Role
            {
                Id = (int)ExistingRoles.User,
                Name = "User"
            }, new Role
            {
                Id = (int)ExistingRoles.Manager,
                Name = "Manager"
            }, new Role
            {
                Id = (int)ExistingRoles.Administrator,
                Name = "Administrator"
            }, new Role
            {
                Id = (int)ExistingRoles.Owner,
                Name = "Owner"
            });

        modelBuilder.Entity<User>()
            .HasData(new User
            {
                Id = 1,
                Portfolios = "",
                RoleId = (int)ExistingRoles.Owner,
                SkillLevel = null,
                Skills = "",
                TelegramUserId = 502351239
            });
        
        base.OnModelCreating(modelBuilder);
    }
}