using TalentForge.TelegramBotUI.Common.Executors.RegistrationDialog;
using Telegram.Bot.Types.ReplyMarkups;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Sequence.Service;

namespace TalentForge.TelegramBotUI.Common.Executors;

public class BaseExecutor : Executor
{
   private readonly ISequenceService _sequenceService;
   private readonly InlineKeyboardBuilder _keyboardBuilder;

   public BaseExecutor(ISequenceService sequenceService, InlineKeyboardBuilder keyboardBuilder)
   {
      _sequenceService = sequenceService;
      _keyboardBuilder = keyboardBuilder;
   }

   [TargetCommand]
   public async Task Start()
   {
      await Client.SendTextMessageAsync(
         "Привіт, це бот проєкту TalentForge...", 
         replyMarkup: _keyboardBuilder
            .Button("😋 Стати учасником", nameof(StartRegistration))
            .Build());
   }

   [TargetCallbackData]
   public async Task StartRegistration()
   {
      await _sequenceService.StartAsync<RegistrationSequence>();
   }
}