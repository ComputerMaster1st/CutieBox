using System;
using System.Net.Http;

namespace CutieBox.API.RandomApi
{
    public abstract class RandomApiBase
    {
        protected readonly HttpClient RandomApiClient;

        protected RandomApiBase() => RandomApiClient = new()
        {
            BaseAddress = new Uri("https://some-random-api.ml/")
        };
    }
}
