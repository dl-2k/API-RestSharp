using Core.Utilities;
using DemoQA.Test.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.Test.DataProvider
{
    public class BookProvider
    {
        private static readonly Dictionary<string, BookDto> _bookDto;
        static BookProvider()
        {
            _bookDto = JsonUtils.ReadDictionaryJson<BookDto>("TestData/Book/book.json");
        }
        public static BookDto GetBookInfoData(string key)
        {
            if (_bookDto.ContainsKey(key))
                return _bookDto[key];

            return null;
        }
    }
}
