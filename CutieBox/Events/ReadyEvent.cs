using CutieBox.Core.Attributes;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace CutieBox.Events
{
    [PreInitialize]
    public class ReadyEvent
    {
        private readonly DiscordSocketClient _client;

        public ReadyEvent(DiscordSocketClient client)
        {
            client.Ready += Client_Ready;
            _client = client;
        }

        private async Task Client_Ready()
        {
            await _client.SetStatusAsync(UserStatus.Online);
            await _client.SetGameAsync(string.Format("Online In {0} Servers!", _client.Guilds.Count));
        }
    }
}
