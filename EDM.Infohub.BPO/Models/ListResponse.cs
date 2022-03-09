using Newtonsoft.Json;
using System.Collections.Generic;

namespace EDM.Infohub.BPO.Models
{
    public class ListResponse<T>
    {
        [JsonProperty(PropertyName = "items")]
        public List<T> Items { get; set; }

        [JsonProperty(PropertyName = "hasNext")]
        public bool HasNext { get; set; }

    }

}
