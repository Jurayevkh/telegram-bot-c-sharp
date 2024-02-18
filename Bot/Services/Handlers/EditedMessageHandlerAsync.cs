using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Services;

public partial class BotUpdateHandler
{
    private async Task EditedMessageHandlerAsync(ITelegramBotClient botClient, Message? message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        var user = message.From;
        _logger.LogInformation($"{user.FirstName} was edited their:\"{message.Text}\" message");
    }
}

