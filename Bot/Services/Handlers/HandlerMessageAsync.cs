using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Services;

public partial class BotUpdateHandler
{
    public async Task HandlerMessageAsync(ITelegramBotClient botClient, Message? message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        var user = message.From;
        _logger.LogInformation($"From {user.FirstName} was received new message");
    }
}
