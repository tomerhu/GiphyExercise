using GiphyWebApi.Interfaces;
using GiphyWebApi.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GiphyWebApi.Tools
{
    public class WebTools : IWebTools
    {
        private readonly ILogger _logger;

        public WebTools(ILogger<WebTools> logger)
        {
            _logger = logger;
        }

        public async Task<WebResult> GetData(Uri uri)
        {
            _logger.LogInformation($"Start GetData at {DateTime.UtcNow.ToLongTimeString()}");
            using (var httpClient = new HttpClient())
            {
                try
                {
                    WebResult webResult = new WebResult(false);
                    var response = await httpClient.GetAsync(uri);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    webResult.Result = responseContent;
                    webResult.IsSuccess = response.IsSuccessStatusCode;

                    _logger.LogInformation($"GetData return success: {webResult.IsSuccess}");

                    return webResult;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"GetData Exception: {ex.Message}");
                    return new WebResult(false, ex.Message);
                }
            }
        }

        /// <summary>
        /// Get a model class of url properties and translate it to formatted URL
        /// </summary>
        /// <param name="obj">The model class</param>
        /// <returns>A formatted URL</returns>
        public static string ToKeyValueURL(object obj)
        {
            var keys = obj.GetType().GetProperties().
                SelectMany(p => p.GetCustomAttributes(typeof(JsonPropertyAttribute), true))
                .Cast<JsonPropertyAttribute>().Select(j => j.PropertyName).ToArray();

            var values = obj.GetType().GetProperties().ToList().
                Select(p => p.GetValue(obj)).ToArray();

            var keyValuePairs = Enumerable.Zip(keys, values,
                (key, value) => new KeyValuePair<string, object>(key, value)).ToList().Select(
                k => $"{k.Key}={k.Value}").ToArray();            

            return string.Join('&', keyValuePairs);            
        }
    }
}
