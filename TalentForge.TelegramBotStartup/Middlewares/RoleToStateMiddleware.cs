using TalentForge.Application.Services.User;
using Telegramper.Core.Configuration.Middlewares;
using Telegramper.Core.Context;
using Telegramper.Core.Delegates;
using Telegramper.Executors.QueryHandlers.UserState;

namespace TalentForge.TelegramBotStartup.Middlewares;

public class RoleToStateMiddleware : IMiddleware
{
    private readonly IUserService _userService;
    private readonly IUserStates _userStates;

    public RoleToStateMiddleware(IUserService userService, IUserStates userStates)
    {
        _userService = userService;
        _userStates = userStates;
    }

    public async Task InvokeAsync(UpdateContext updateContext, NextDelegate next)
    {
        if (updateContext.TelegramUserId == null)
            return;
        
        var role = await _userService.GetRoleByTelegramUserId(updateContext.TelegramUserId!.Value);
        if (await _userStates.Contains(role.Name) == false)
            await _userStates.AddAsync(role.Name);

        await next();
    }
}