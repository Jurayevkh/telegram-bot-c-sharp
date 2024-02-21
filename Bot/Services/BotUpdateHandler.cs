﻿using System.Globalization;
using Bot.Resources;
using Microsoft.Extensions.Localization;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Services;

public partial class BotUpdateHandler : IUpdateHandler
{
    private readonly ILogger<BotUpdateHandler> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private IStringLocalizer _localizer;

    public BotUpdateHandler(ILogger<BotUpdateHandler> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Our bot has a new error sir :) : {exception.Message}");
        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        using var scope=_scopeFactory.CreateScope();

        var culture = new CultureInfo("uz-Uz");
        CultureInfo.CurrentCulture=culture;
        CultureInfo.CurrentUICulture=culture;

        _localizer=scope.ServiceProvider.GetRequiredService<IStringLocalizer<BotLocalizer>>();
        var handler = update.Type switch
        {
            UpdateType.Message => HandlerMessageAsync(botClient, update.Message, cancellationToken),
            UpdateType.EditedMessage => EditedMessageHandlerAsync(botClient, update.Message, cancellationToken),
            _ => UnknownUpdateHandler(botClient, update.Message, cancellationToken)
        };
        try
        {
        await handler;
        }
        catch(Exception e)
        {
            await HandlePollingErrorAsync(botClient,e,cancellationToken);
        }
    }

    private Task UnknownUpdateHandler(ITelegramBotClient botClient, Message? message, CancellationToken cancellationToken)
    {
        var user = message.From;
        _logger.LogInformation($"The user {user.FirstName} was send some unknown message!");
        return Task.CompletedTask;
    }
}

