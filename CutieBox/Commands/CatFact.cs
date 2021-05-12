using CutieBox.API.RandomApi;
using Discord.Commands;
using System.Threading.Tasks;

namespace CutieBox.Commands
{
    [Group("catfact")]
    public class CatFact : ModuleBase
    {
        private readonly RandomApi _randomApi;

        public CatFact(RandomApi randomApi)
            => _randomApi = randomApi;

        [Command]
        public async Task ExecuteAsync()
        {
            var response = await _randomApi.Facts.GetCatAsync();

            await ReplyAsync(response.Fact);
        }
    }
}
