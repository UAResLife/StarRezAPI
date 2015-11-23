
using Newtonsoft.Json;

namespace StarRezAPI
{
    [JsonObject]
    public class Application
    {
        [JsonProperty("StudentID")]
        public string StudentId { get; set; }

        [JsonProperty("Application_Term")]
        public string Term { get; set; }

        [JsonProperty("Term_Description")]
        public string TermDescription { get; set; }

        [JsonProperty("Application_Rating")]
        public string Rating { get; set; }

        [JsonProperty("Application_Status")]
        public string Status { get; set; }

        [JsonProperty("Application_Fee_Status")]
        public string FeeStatus { get; set; }

        [JsonProperty("License_Agreement_Status")]
        public string LicenseAgreementStatus { get; set; }

        [JsonProperty("Confirmation_Rent_Payment_Status")]
        public string CrpStatus { get; set; }
    }
}
