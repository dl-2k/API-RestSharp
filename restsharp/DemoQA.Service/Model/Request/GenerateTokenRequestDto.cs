using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Service.Model.Request
{
    public class GenerateTokenRequestDto
    {
        [JsonProperty("userName")]
        public string userName { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }
    }
}
