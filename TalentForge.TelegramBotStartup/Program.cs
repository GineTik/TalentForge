using System.Reflection;
using Microsoft.Extensions.Logging;
using TalentForge.Application;
using TalentForge.Infrastructure;
using TalentForge.TelegramBotStartup.Middlewares;
using Telegramper.Core;
using Telegramper.Executors.Initialization;
using Telegramper.Executors.Initialization.Services;
using Telegramper.Executors.QueryHandlers.Middleware;

var builder = new BotApplicationBuilder();
builder.Services.AddExecutors(options =>
{
    options.Assemblies = new[]
    {
        new SmartAssembly(Assembly.Load("TalentForge.TelegramBotUI.Common")),
    };
    
    options.ParametersParser.ErrorMessages.TypeParseError = "❌ Невірні параметри";
    options.ParametersParser.ErrorMessages.ArgsLengthIsLess = "❌ Недостатньо параметрів";
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();
app.UseMiddleware<RoleToStateMiddleware>();
app.UseMiddleware<AnswerCallbackMiddleware>();
app.UseExecutors();

app.RunPolling();