using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Model.Response
{
    public class ReplaceBookResponeDto
    {
        [JsonProperty("userId")]
        public string userId { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("books")]
        public List<BookResponeDto> Books { get; set; }
    }
}
