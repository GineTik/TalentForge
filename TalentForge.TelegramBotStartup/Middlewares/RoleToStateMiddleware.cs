using Microsoft.Extensions.Options;
using TalentForge.Application.Services.User;
using Telegramper.Core.Configuration.Middlewares;
using Telegramper.Core.Context;
using Telegramper.Core.Delegates;
using Telegramper.Executors.Common.Options;
using Telegramper.Executors.QueryHandlers.UserState;

namespace TalentForge.TelegramBotStartup.Middlewares;

public class RoleToStateMiddleware : IMiddleware
{
    private readonly IUserService _userService;
    private readonly IUserStates _userStates;
    private readonly UserStateOptions _userStateOptions;

    private static string RoleModificator => "Role:";
    
    public RoleToStateMiddleware(IUserService userService, IUserStates userStates, IOptions<UserStateOptions> userStateOptions)
    {
        _userService = userService;
        _userStates = userStates;
        _userStateOptions = userStateOptions.Value;
    }

    public async Task InvokeAsync(UpdateContext updateContext, NextDelegate next)
    {
        if (updateContext.TelegramUserId == null)
            return;
        
        var role = await _userService.GetRoleByTelegramUserId(updateContext.TelegramUserId!.Value);
        if (role != null && await _userStates.Contains(RoleModificator + role.Name) == false)
        {
            await _userStates.AddAsync(RoleModificator + role.Name);
            await _userStates.AddAsync(_userStateOptions.DefaultUserState);
        }

        await next();
    }
}