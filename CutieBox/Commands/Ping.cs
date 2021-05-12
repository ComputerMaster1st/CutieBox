using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace CutieBox.Commands
{
    [Group("ping")]
    public class Ping : ModuleBase
    {
        private readonly DiscordSocketClient _client;

        public Ping(DiscordSocketClient client)
            => _client = client;

        [Command]
        public Task ExecuteAsync()
            => ReplyAsync(string.Format("Ping To Discord: {0}", _client.Latency));
    }
}
