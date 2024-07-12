using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Model.Request
{
    public class DeleteBookResquestDto
    {
        [JsonProperty("isbn")]
        public string isbn { get; set; }
        
        [JsonProperty("userId")]
        public string userId { get; set; }
    }
}
