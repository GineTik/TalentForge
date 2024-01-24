using TalentForge.Domain.DTOs.User;

namespace TalentForge.Application.Services.User;

public interface IUserService
{
    Task<int> RegisterUser(RegistrationUser user);
    Task<UserProfile> GetUserProfile(long telegramUserId);
    Task<RoleDTO?> GetRoleByTelegramUserId(long telegramUserId);
}