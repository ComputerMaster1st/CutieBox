using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CutieBox.API.RandomApi
{
    public class RandomApiImage : RandomApiBase
    {
        private const string Endpoint = "img";

        public async Task<RedPandaImageResponse> GetRedPandaAsync()
        {
            var json = await RandomApiClient.GetStringAsync($"{Endpoint}/red_panda");

            return JsonConvert.DeserializeObject<RedPandaImageResponse>(json);
        }
    }
}
