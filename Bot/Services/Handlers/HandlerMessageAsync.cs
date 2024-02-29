using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Services;

public partial class BotUpdateHandler
{
    public async Task HandlerMessageAsync(ITelegramBotClient botClient, Message? message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        var user = message.From;
        _logger.LogInformation($"From {user.FirstName} was received new message");

        var handler = message.Type switch
        {
            MessageType.Text => HandleTextMessageAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        await handler;
    }

    private async Task HandleUnknownMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Received message with type {message.Type}");
        await Task.CompletedTask;
    }

    private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var user = message.From;
        await botClient.SendTextMessageAsync
            (
            chatId:user.Id,
            text:_localizer["start"],
            replyToMessageId:message.MessageId,
            cancellationToken:cancellationToken
            );
        
    }
}
