using System;
using Newtonsoft.Json;

namespace MarsImages.Internal.Data.Models
{
    public class Camera
    {
        public Camera()
        { }
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("rover_id")]
        public int RoverId { get; set; }
    }
}