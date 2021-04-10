using System;
using System.Net;
using System.Threading.Tasks;
using GiphyWebApi.Interfaces;
using GiphyWebApi.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GiphyWebApi.Tools
{
    public class GiphyTools :IGiphyTools
    {
        private readonly IWebTools _webTools;
        private readonly string _giphyUrl;
        private readonly string _apiKey;
        private readonly ILogger _logger;

        public GiphyTools(GiphyConfig giphyConfig, ILogger<GiphyTools> logger, IWebTools webTools)
        {
            _apiKey = giphyConfig.ApiKey;
            _giphyUrl = giphyConfig.GiphyUrl;
            _webTools = webTools;
            _logger = logger;
        }

        public async Task<GifResult> GifFetch(string searchTerm)
        {
            _logger.LogInformation($"GifFetch search for {searchTerm} at {DateTime.UtcNow.ToLongTimeString()}");
            if (string.IsNullOrEmpty(searchTerm))
            {
                throw new FormatException("query term is required");
            }
            SearchParameters searchParameters = new SearchParameters();
            searchParameters.ApiKey = _apiKey;
            searchParameters.Query = searchTerm;

            var searchString = WebTools.ToKeyValueURL(searchParameters);

            var result = await _webTools.GetData(new Uri($"{_giphyUrl}{searchString}"));

            if (!result.IsSuccess)
            {
                string message = $"GifFetch Failed to get GIFs: {result.Result}";
                _logger.LogError(message);
                throw new WebException(message);
            }

            GifResult gifResult = JsonConvert.DeserializeObject<GifResult>(result.Result);
            return gifResult;
        }
    }
}
