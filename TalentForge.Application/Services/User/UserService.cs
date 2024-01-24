using Microsoft.EntityFrameworkCore;
using TalentForge.Application.Interfaces;
using TalentForge.Domain.DTOs.User;

namespace TalentForge.Application.Services.User;

public class UserService : IUserService
{
    private readonly IDataContext _dataContext;

    public UserService(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Task<int> RegisterUser(RegistrationUser user)
    {
        throw new NotImplementedException();
    }

    public Task<UserProfile> GetUserProfile(long telegramUserId)
    {
        throw new NotImplementedException();
    }

    public async Task<RoleDTO?> GetRoleByTelegramUserId(long telegramUserId)
    {
        var userByTelegramUserId = await _dataContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.TelegramUserId == telegramUserId);

        return userByTelegramUserId?.Role == null
            ? null
            : new RoleDTO
            {
                Name = userByTelegramUserId?.Role.Name!
            };
    }
}