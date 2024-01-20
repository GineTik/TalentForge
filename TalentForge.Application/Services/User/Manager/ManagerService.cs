using TalentForge.Domain.DTOs.User;

namespace TalentForge.Application.Services.User.Manager;

public class ManagerService : IManagerService
{
    public Task<UserProfileForManager> GetUserProfileForTest(int id)
    {
        throw new NotImplementedException();
    }
}