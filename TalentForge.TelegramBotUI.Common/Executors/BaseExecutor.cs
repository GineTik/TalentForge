using TalentForge.Application.Services.User;
using TalentForge.TelegramBotUI.Common.Executors.RegistrationDialog;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Executors.QueryHandlers.Attributes.Validations;
using Telegramper.Sequence.Service;

namespace TalentForge.TelegramBotUI.Common.Executors;

public class BaseExecutor : Executor
{
   private readonly ISequenceService _sequenceService;
   private readonly InlineKeyboardBuilder _keyboardBuilder;
   private readonly IUserService _userService;

   public BaseExecutor(ISequenceService sequenceService, InlineKeyboardBuilder keyboardBuilder, IUserService userService)
   {
      _sequenceService = sequenceService;
      _keyboardBuilder = keyboardBuilder;
      _userService = userService;
   }

   [TargetCommand]
   public async Task Start()
   {
      await Client.SendTextMessageAsync(
         "Привіт, це бот проєкту TalentForge...", 
         replyMarkup: _keyboardBuilder
            .Button(TargetTextConstants.BecomeMember, nameof(StartRegistration)).EndRow()
            .Button(TargetTextConstants.OfferCollaboration, nameof(OfferCollaboration)).EndRow()
            .Build());
   }

   [TargetCallbackData]
   public async Task StartRegistration()
   {
      await _sequenceService.StartAsync<RegistrationSequence>();
   }

   [TargetText(TargetTextConstants.PrintProfile)]
   [RequiredData(UpdateProperty.User)]
   public async Task PrintProfile()
   {
      var profile = await _userService.GetUserProfile(UpdateContext.TelegramUserId!.Value);
   }

   [TargetCallbackData]
   public async Task OfferCollaboration()
   {
      await Client.SendTextMessageAsync(
         "✨ Якщо вам не вистачає людей в команду, потрібен дизайн сайту або ви " +
             "підприємець, який бажає отримати крутий проєкт? Пиши мені в приватні, " +
             "щоб домовитись!\n\n↪ @GineTik0_0");
   }
}