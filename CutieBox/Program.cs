using CutieBox.Core.Config;
using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace CutieBox
{
    class Program
    {
        private readonly DiscordSocketClient _client = new(
                new()
                {
                    DefaultRetryMode = RetryMode.AlwaysRetry,
                    LogLevel = LogSeverity.Info,
                    AlwaysDownloadUsers = true
                }
            );

        private Config _config = null;
        private BootLoader _bootLoader = null;

        static void Main() => new Program().RunAsync().GetAwaiter().GetResult();

        // Main Bot Thread/Process
        private async Task RunAsync()
        {
            // Load Config
            _config = await Config.LoadAsync();
            if (_config == null)
            {
                Console.WriteLine("Config file has been generated! Please fill in the gaps inside the \"{0}\"!", Config.Filename);
                return;
            }

            // ACTIVATE BOOT SEQUENCE!
            _bootLoader = new(_config, _client);

            await _bootLoader.StartAsync();

            // Connect To Discord
            await _client.LoginAsync(TokenType.Bot, _config.Token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}
