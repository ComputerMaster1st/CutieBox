using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CutieBox.API.RandomApi
{
    public class RandomApiFact : RandomApiBase
    {
        private const string Endpoint = "facts";

        public async Task<CatFactResponse> GetCatAsync()
        {
            var json = await RandomApiClient.GetStringAsync($"{Endpoint}/cat");

            return JsonConvert.DeserializeObject<CatFactResponse>(json);
        }
    }
}
