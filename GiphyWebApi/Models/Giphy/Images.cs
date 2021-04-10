﻿using Newtonsoft.Json;

namespace GiphyWebApi.Models
{
    /// <summary>
    /// Images object
    /// Get only the neccessary properties
    /// </summary>
    public class Images
    {
        //[JsonProperty("fixed_height")]
        //public FixedHeight FixedHeight { get; set; }

        //[JsonProperty("fixed_height_still")]
        //public FixedHeightStill FixedHeightStill { get; set; }

        //[JsonProperty("fixed_height_downsampled")]
        //public FixedHeightDownsampled FixedHeightDownsampled { get; set; }

        //[JsonProperty("fixed_width")]
        //public FixedWidth FixedWidth { get; set; }

        //[JsonProperty("fixed_width_still")]
        //public FixedWidthStill FixedWidthStill { get; set; }

        //[JsonProperty("fixed_width_downsampled")]
        //public FixedWidthDownsampled FixedWidthDownsampled { get; set; }

        //[JsonProperty("fixed_height_small")]
        //public FixedHeightSmall FixedHeightSmall { get; set; }

        //[JsonProperty("fixed_height_small_still")]
        //public FixedHeightSmallStill FixedHeightSmallStill { get; set; }

        //[JsonProperty("fixed_width_small")]
        //public FixedWidthSmall FixedWidthSmall { get; set; }

        //[JsonProperty("fixed_width_small_still")]
        //public FixedWidthSmallStill FixedWidthSmallStill { get; set; }

        //[JsonProperty("downsized")]
        //public Downsized Downsized { get; set; }

        //[JsonProperty("downsized_still")]
        //public DownsizedStill DownsizedStill { get; set; }

        //[JsonProperty("downsized_large")]
        //public DownsizedLarge DownsizedLarge { get; set; }

        [JsonProperty("original")]
        public Original Original { get; set; }

        //[JsonProperty("original_still")]
        //public OriginalStill OriginalStill { get; set; }
    }
}
