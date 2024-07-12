using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Test.DataObjects
{
    public class BookDto
    {
        [JsonProperty("isbn")]
        public string isbn { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("author")]
        public string author { get; set; }

        [JsonProperty("publish_date")]
        public DateTime publish_date { get; set; }

        [JsonProperty("publisher")]
        public string publisher { get; set; }

        [JsonProperty("pages")]
        public int pages { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("website")]
        public string website { get; set; }
    }
}
