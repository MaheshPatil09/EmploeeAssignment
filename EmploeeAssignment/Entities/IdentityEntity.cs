﻿using EmploeeAssignment.Common;
using Newtonsoft.Json;

namespace EmploeeAssignment.Entities
{
    public class IdentityEntity : BaseEntity
    {
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
