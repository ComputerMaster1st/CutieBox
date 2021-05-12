using CutieBox.API.RandomApi;
using Discord.Commands;
using System.Threading.Tasks;

namespace CutieBox.Commands
{
    [Group("redpanda")]
    public class RedPanda : ModuleBase
    {
        private readonly RandomApi _randomApi;

        public RedPanda(RandomApi randomApi)
            => _randomApi = randomApi;

        [Command]
        public async Task ExecuteAsync()
        {
            var response = await _randomApi.Image.GetRedPandaAsync();

            await ReplyAsync(response.Link);
        }
    }
}
