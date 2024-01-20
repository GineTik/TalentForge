using System.Reflection;
using Microsoft.Extensions.Logging;
using Telegramper.Core;
using Telegramper.Executors.Initialization;
using Telegramper.Executors.Initialization.Services;
using Telegramper.Executors.QueryHandlers.Middleware;

var builder = new BotApplicationBuilder();
builder.ConfigureApiKey("6893237146:AAFwJudGjf5VstS43NWRUJRBWu_5-T1MhZQ");
builder.Services.AddExecutors(options =>
{
    options.Assemblies = new[]
    {
        new SmartAssembly(Assembly.Load("TalentForge.TelegramBotUI.Common")),
    };
    
    options.ParametersParser.ErrorMessages.TypeParseError = "❌ Невірні параметри";
    options.ParametersParser.ErrorMessages.ArgsLengthIsLess = "❌ Недостатньо параметрів";
});
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();
app.UseExecutors();

app.RunPolling();