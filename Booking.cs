using Newtonsoft.Json;

namespace StarRezAPI
{
    public class Booking
    {
        [JsonProperty("StudentID ")]
        public string StudentId { get; set; }

        [JsonProperty("Term_Code")]
        public string Term { get; set; }

        [JsonProperty("Hall_Name")]
        public string HallName { get; set; }

        [JsonProperty("Room_Type")]
        public string RoomType { get; set; }

        [JsonProperty("Term_Description")]
        public string TermDescription { get; set; }

        [JsonProperty("Booking_Status")]
        public string Status { get; set; }
    }
}
