using Newtonsoft.Json;

namespace EmploeeAssignment.Dto
{
    public class IdentityDto
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "pan", NullValueHandling = NullValueHandling.Ignore)]
        public string PAN { get; set; }
        [JsonProperty(PropertyName = "aadhar", NullValueHandling = NullValueHandling.Ignore)]
        public string Aadhar { get; set; }
        [JsonProperty(PropertyName = "nationality", NullValueHandling = NullValueHandling.Ignore)]
        public string Nationality { get; set; }
        [JsonProperty(PropertyName = "passportNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PassportNumber { get; set; }
        [JsonProperty(PropertyName = "pFNumber ", NullValueHandling = NullValueHandling.Ignore)]
        public string PFNumber { get; set; }

    }
}
