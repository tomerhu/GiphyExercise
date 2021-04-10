using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiphyApp.Models;
using Microsoft.Extensions.Configuration;
using GiphyWebApi.Models;
using System;
using GiphyWebApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

namespace GiphyApp.Controllers
{
    [Route("api/Giphy")]
    [ApiController]
    public class GiphyController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IConfiguration Configuration;
        private readonly IGiphyTools _giphyTools;
        private readonly GiphyConfig _giphyConfig;
        private IMemoryCache _cache;

        public GiphyController(ILogger<GiphyController> logger, IConfiguration configuration, 
            IGiphyTools giphyTools, GiphyConfig giphyConfig, IMemoryCache memoryCache)
        {
            _logger = logger;
            _giphyConfig = giphyConfig;
            Configuration = configuration;
            _giphyTools = giphyTools;
            _cache = memoryCache;
        }

        // GET: api/Giphy/cat
        [HttpGet("{searchTerm}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<GiphyItem>> GetGiphyItem(string searchTerm)
        {
            _logger.LogInformation($"GetGiphyItem search for {searchTerm} at {DateTime.UtcNow.ToLongTimeString()}");

            GiphyItem giphyItem = new GiphyItem();
            try
            {
                if(!_cache.TryGetValue(searchTerm, out giphyItem))
                {
                    GifResult gifResult = await _giphyTools.GifFetch(searchTerm);
                    if (gifResult != null)
                    {
                        giphyItem = new GiphyItem()
                        {
                            SearchTerm = searchTerm,
                            Url = gifResult.Data.First().Images.Original.Url
                        };

                        var cacheEntryOptions = new MemoryCacheEntryOptions();

                        _cache.Set(searchTerm, giphyItem, cacheEntryOptions);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetGiphyItem Exception: {ex.Message}");
                throw;
            }

            _logger.LogInformation($"GetGiphyItems find gif at url={giphyItem.Url}");

            return giphyItem;
        }
    }
}