using TalentForge.Domain.DTOs.User;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Supports;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Sequence.Attributes;
using Telegramper.Sequence.Service;
using Telegramper.Sessions.Interfaces;

namespace TalentForge.TelegramBotUI.Common.Executors.RegistrationDialog;

public class RegistrationSequence : Executor
{
    private readonly ISequenceService _sequenceService;
    private readonly IUserSession _userSession;
    private readonly InlineKeyboardBuilder _keyboardBuilder;

    public RegistrationSequence(ISequenceService sequenceService, IUserSession userSession, InlineKeyboardBuilder keyboardBuilder)
    {
        _sequenceService = sequenceService;
        _userSession = userSession;
        _keyboardBuilder = keyboardBuilder;
    }

    [StartOfSequence]
    public async Task Start()
    {
        await _userSession.SetAsync(new RegistrationUser
        {
            TelegramUserId = UpdateContext.TelegramUserId!.Value
        });
        await Client.DeleteMessageAsync();
        await Client.SendTextMessageAsync(
            text: "Привіт! Які ваші навички? \n\n" +
                "Наприклад, чи ви є веб-розробником з володінням C#, ASP.NET Core, чи, можливо, ви графічний дизайнер і працюєте в Illustrator? \n\n" +
                "Нас цікавить отримати повний огляд ваших навичок та рівня в них.", 
            replyMarkup: new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton(TargetTextConstants.StopDialog)                
            })
            {
                ResizeKeyboard = true
            });
    }

    [TargetSequenceStep]
    public async Task GetSkills(string skillsAndDescription)
    {
        await _userSession.SetAsync<RegistrationUser>(user => user.Skills = skillsAndDescription);
        await _sequenceService.NextAsync();

        await Client.SendTextMessageAsync("Круто! До речі, у вас є портфоліо?", replyMarkup: _keyboardBuilder
            .Button("Так", "true")
            .Button("Ні", "false")
            .Build());
    }

    [TargetSequenceStep]
    public async Task HavePortfolio(bool havePortfolio)
    {
        await _sequenceService.NextAsync(steps: havePortfolio ? 1 : 2); // offset
        await Client.DeleteMessageAsync();

        if (havePortfolio)
        {
            var message = await Client.SendTextMessageAsync(
                text: "Чудово, ви можете відправити нам силки на ваші роботи, наприклад, на bihance.\n" +
                    "Коли ви закінчите, нажміть на кнопку нижче!",
                replyMarkup: _keyboardBuilder
                    .Button(TargetTextConstants.HavntPortfolio)
                    .Build());
            await _userSession.SetAsync(message);
        }
        else
        {
            await Client.SendTextMessageAsync(
                "Очікуйте, менеджер перевірить ваші навички та надішле тестове завдання.\n" +
                "Після його виконання ви успішно зареєструєтесь.");
        }
    }
    
    [TargetSequenceStep]
    public async Task GetPortfolio(string portfolioLink)
    {
        switch (portfolioLink)
        {
            case TargetTextConstants.HavntPortfolio:
                await _sequenceService.NextAsync();
                await Client.DeleteMessageAsync();
                await Client.SendTextMessageAsync(
                    "Очікуйте, менеджер перевірить ваші навички та надішле тестове завдання.\n" +
                        "Після його виконання ви успішно зареєструєтесь.");
                return;
            
            case TargetTextConstants.NextStep:
                await _sequenceService.NextAsync(2);
                await Client.DeleteCurrentCallbackButtonAsync();
                await Client.SendTextMessageAsync(
                    "Дякую. Ви завершили реєстрацію, наш менеджер перевірить заяву та напише вам результат!", 
                    replyMarkup: new ReplyKeyboardRemove());                
                return;
        }

        if (portfolioLink.StartsWith("https://") == false)
        {
            await Client.SendTextMessageAsync(
                "❌ Не можу розпізнати тут https:// силку", 
                replyToMessageId: UpdateContext.MessageId!);
            return;
        }
        
        await _userSession.SetAsync<RegistrationUser>(user => user.Portfolios.Add(portfolioLink));
        var message = await _userSession.GetAndRemoveAsync<Message>();
        if (message != null) 
        {
            await Client.EditMessageReplyMarkupAsync(
                UpdateContext.ChatId!, 
                message!.MessageId,
                replyMarkup: _keyboardBuilder
                    .Button(TargetTextConstants.NextStep)
                   .Build());
        }
    }
    
    [TargetText(TargetTextConstants.StopDialog)]
    [UserState("Sequence:TalentForge.TelegramBotUI.Common.Executors.RegistrationDialog.RegistrationSequence")]
    public async Task EmdSequence()
    {
        await Client.SendTextMessageAsync("Діалог закінчено", replyMarkup: new ReplyKeyboardRemove());
        await _sequenceService.EndAsync();
    }
}