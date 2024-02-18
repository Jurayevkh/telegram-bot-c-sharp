using Bot;
using Bot.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;

var builder = WebApplication.CreateBuilder(args);

var token = builder.Configuration.GetValue("BotToken",string.Empty);

builder.Services.AddSingleton(p=>new TelegramBotClient(token));
builder.Services.AddSingleton<IUpdateHandler,BotUpdateHandler>();
builder.Services.AddHostedService<BotBackgroundService>();

var app = builder.Build();

app.Run();

