
using Newtonsoft.Json;

namespace StarRezAPI
{
    [JsonObject]
    public class Model
    {
        [JsonProperty("StudentID")]
        public string StudentId { get; set; }
    }
}
