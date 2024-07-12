using Core.Reports;
using DemoQA.Service.Model.Response;
using DemoQA.Service.Services;
using DemoQA.Test.DataProvider;
using FluentAssertions;
using Newtonsoft.Json;


namespace DemoQA.Test.TestCases
{
    [TestFixture, Category("AddBooks")]
    public class AddBookTest : BaseTest
    {
        private BookServices _bookServices;
        private UserProvider _userProvider;
        

        public AddBookTest()
        {
            _bookServices = new BookServices(ApiClient);
            _userProvider = new UserProvider();

        }
        [Test]
        [TestCase("user_01","book1")]
        public async Task AddBookSuccesfully(string userInfokeyData,string bookInfoData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);



            ReportLog.Info("1.Add book into collection");
            var response = await _bookServices.AddBookAsync(userInfo.UserId,bookInfo.isbn,token);
            var responseData = response.Data;
            Console.WriteLine(response.Content);


            ReportLog.Info("2.Verify status code repsonse");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content);

            ReportLog.Info("3.Assert add book repsonse");
            response.Data.Books.FirstOrDefault().isbn.Should().Be(bookInfo.isbn);

            ReportLog.Info("4. Delte initial book has been add into collection");
            _bookServices.DeleteBookAsync(userInfo.UserId, bookInfo.isbn, token);

        }

        [Test]
        [TestCase("user_01", "book1")]
        public async Task AddBookToCollectionWithoutAuthotized(string userInfokeyData, string bookInfoData)
        {

            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);


            ReportLog.Info("1.Add book into collection");
            var response = await _bookServices.AddBookAsync(userInfo.UserId, bookInfo.isbn, null);
            
            ReportLog.Info("2.Verify status code repsonse");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            
            ReportLog.Info("3.Assert add book repsonse");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((int)result["code"]).Should().Be(1200);
            ((string)result["message"]).Should().Be("User not authorized!");
        }
        [Test]
        [TestCase("user_01", "book1")]

        public async Task AddBookToCollectionWithWrongUserId(string userInfokeyData, string bookInfoData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);

            ReportLog.Info("1.Add book into collection");
            var response = await _bookServices.AddBookAsync(userInfo.UserId + "456757", bookInfo.isbn,token);
            ReportLog.Info("2.Verify status code repsonse");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            
            ReportLog.Info("3.Assert add book repsonse");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((int)result["code"]).Should().Be(1207);
            ((string)result["message"]).Should().Be("User Id not correct!");
        }
        [Test]
        [TestCase("user_01", "book1")]

        public async Task AddBookToCollectionWithWrongBookId(string userInfokeyData, string bookInfoData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);

            ReportLog.Info("1.Add book into collection");
            var response = await _bookServices.AddBookAsync(userInfo.UserId, bookInfo.isbn + "54654", token);
            
            ReportLog.Info("2.Verify status code repsonse");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            
            ReportLog.Info("3.Assert add book repsonse");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((int)result["code"]).Should().Be(1205);
            ((string)result["message"]).Should().Be("ISBN supplied is not available in Books Collection!");
        }
    }
}

