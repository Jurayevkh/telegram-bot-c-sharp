using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Services;

public partial class BotUpdateHandler : IUpdateHandler
{
    private readonly ILogger<BotUpdateHandler> _logger;

    public BotUpdateHandler(ILogger<BotUpdateHandler> logger)
    {
        _logger = logger;
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Our bot has a new error sir :) : {exception.Message}");
        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
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

