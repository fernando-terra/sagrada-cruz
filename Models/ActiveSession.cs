using Newtonsoft.Json;
using System;

namespace br.com.sagradacruz.Models
{
    public class ActiveSession
    {
        [JsonProperty("User")]
        public string User { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Created")]
        public string Created { get; set; }
        [JsonProperty("Status")]
        public bool Status { get; set; }
    }
}
