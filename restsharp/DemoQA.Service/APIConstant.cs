using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoQA.Service
{
    public class APIConstant
    {
        public const string GetUserEndPoint = "/Account/v1/User/{0}";
        public const string AddBookToCollectionEndPoint = "/BookStore/v1/Books";
        public const string DeleteBookFromCollectionEndPoint = "/BookStore/v1/Book";
        public const string ReplaceBookFromCollectionEndPoint = "/BookStore/v1/Books/{0}";
        public const string GenerateToken = "Account/v1/GenerateToken";
    }
}