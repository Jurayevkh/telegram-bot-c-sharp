using Telegram.Bot;

namespace Bot;

public class StartingBotBackgroundService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly ITelegramBotClient _client;

    public StartingBotBackgroundService(ITelegramBotClient client, ILogger logger)
    {
        _client = client;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var bot=await _client.GetMeAsync(stoppingToken);

        _logger.LogInformation($"The bot {bot.Username} is succesfully started");
    }
}
