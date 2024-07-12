using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Model.Request
{
    public class CollectionOfIsbn
    {
        [JsonProperty("isbn")]
        public string isbn { get; set; }
    }

    public class AddBookRequestDto
    {
        [JsonProperty("userId")]
        public string userId { get; set; }

        [JsonProperty("collectionOfIsbns")]
        public List<CollectionOfIsbn> CollectionOfIsbns { get; set; }
    }
}

