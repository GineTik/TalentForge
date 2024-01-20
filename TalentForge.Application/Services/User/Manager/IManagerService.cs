using TalentForge.Domain.DTOs.User;

namespace TalentForge.Application.Services.User.Manager;

public interface IManagerService
{
    Task<UserProfileForManager> GetUserProfileForTest(int id);
}