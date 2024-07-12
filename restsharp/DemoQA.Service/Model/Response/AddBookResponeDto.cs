using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoQA.Service.Model.Response
{
    public class BookResponse
    {
        [JsonProperty("isbn")]
        public string isbn { get; set; }
    }

    public class AddBookResponseDto
    {
        [JsonProperty("books")]
        public List<BookResponse> Books { get; set; }
    }

}
