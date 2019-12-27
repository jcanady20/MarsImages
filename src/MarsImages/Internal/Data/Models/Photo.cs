using System;
using Newtonsoft.Json;

namespace MarsImages.Internal.Data.Models
{
    public class Photo
    {
        public Photo()
        { }
        public int Id { get; set; }
        public Camera Camera { get; set; }
        public int Sol { get; set; }
        [JsonProperty("img_src")]
        public string Source { get; set; }
        public string LocalSource { get; set; }
        [JsonProperty("earth_date")]
        public DateTime Date { get; set; }
        public Rover Rover { get; set; }
        public string FileName => System.IO.Path.GetFileName(Source);
    }
}