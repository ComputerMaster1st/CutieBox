using Newtonsoft.Json;

namespace CutieBox.API.RandomApi
{
    public class RedPandaImageResponse
    {
        [JsonProperty]
        public string Link { get; private set; } = string.Empty;

        [JsonConstructor]
        private RedPandaImageResponse() { }
    }
}
