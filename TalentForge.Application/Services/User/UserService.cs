using TalentForge.Domain.DTOs.User;

namespace TalentForge.Application.Services.User;

public class UserService : IUserService
{
    public Task<int> RegisterUser(RegistrationUser user)
    {
        throw new NotImplementedException();
    }

    public Task<UserProfile> GetUserProfile(int id)
    {
        throw new NotImplementedException();
    }
}