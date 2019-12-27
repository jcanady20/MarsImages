using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MarsImages.Internal.Data.Models
{
    public class Rover
    {
        public Rover ()
         { }

        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("landing_date")]
        public DateTime LandingDate { get; set; }
        [JsonProperty("launch_date")]
        public DateTime LaunchDate { get; set; }
        public string Status { get; set; }
        [JsonProperty("max_sol")]
        public int MaxSol { get; set; }
        [JsonProperty("max_date")]
        public DateTime MaxDate { get; set; }
        [JsonProperty("total_photos")]
        public int TotalPhotos { get; set; }
        public IReadOnlyList<Camera> Cameras { get; set; }
    }
}