using Microsoft.Extensions.DependencyInjection;
using TalentForge.Application.Services.User;
using Telegramper.Core.Context;
using Telegramper.Executors.QueryHandlers.Attributes.BaseAttributes;

namespace TalentForge.TelegramBotStartup.Attributes.Validation;

public class RequireRoleAttribute : ValidationAttribute
{
    private readonly string _role;

    public RequireRoleAttribute(string role)
    {
        _role = role;
    }

    public override async Task<bool> ValidateAsync(UpdateContext updateContext, IServiceProvider provider)
    {
        var userStates = provider.GetRequiredService<IUserService>();
        return (await userStates.GetRoleByTelegramUserId(updateContext.TelegramUserId!.Value)).Name == _role;
    }
}