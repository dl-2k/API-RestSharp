using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Model.Request
{
    public class ReplaceBookRequestDto
    {
        [JsonProperty("userId")]
        public string userId { get; set; }
        
        [JsonProperty("isbn")]
        public string isbn { get; set; }
    }

}
