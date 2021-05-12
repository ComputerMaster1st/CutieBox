using Newtonsoft.Json;

namespace CutieBox.API.RandomApi
{
    public class CatFactResponse
    {
        [JsonProperty]
        public string Fact { get; private set; } = string.Empty;

        [JsonConstructor]
        private CatFactResponse() { }
    }
}
