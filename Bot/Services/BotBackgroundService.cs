using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Bot;

public class BotBackgroundService : BackgroundService
{
    private readonly ILogger<BotBackgroundService> _logger;
    private readonly TelegramBotClient _client;
    private readonly IUpdateHandler _updatehandler;

    public BotBackgroundService(ILogger<BotBackgroundService> logger, TelegramBotClient client, IUpdateHandler updatehandler)
    {
        _logger = logger;
        _client = client;
        _updatehandler = updatehandler;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var bot = await _client.GetMeAsync(stoppingToken);
        _logger.LogInformation($"Bot successfully started. {bot.Username}");

        _client.StartReceiving(
            _updatehandler.HandleUpdateAsync,
            _updatehandler.HandlePollingErrorAsync,
            new ReceiverOptions() {ThrowPendingUpdates=true},stoppingToken);
    }
}
