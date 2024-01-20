using TalentForge.Domain.DTOs;

namespace TalentForge.Application.Services.User.Administrator;

public interface IAdministratorService
{
    Task SendRequestForProject(RequestForProject request);
}