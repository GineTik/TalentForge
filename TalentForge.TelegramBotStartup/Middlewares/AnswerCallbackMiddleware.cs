using Telegram.Bot.Types.Enums;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Configuration.Middlewares;
using Telegramper.Core.Context;
using Telegramper.Core.Delegates;

namespace TalentForge.TelegramBotStartup.Middlewares;

public class AnswerCallbackMiddleware : IMiddleware
{
    public async Task InvokeAsync(UpdateContext updateContext, NextDelegate next)
    {
        if (updateContext.Update.Type == UpdateType.CallbackQuery)
        {
            await updateContext.Client.AnswerCallbackQueryAsync();
        }

        await next();
    }
}