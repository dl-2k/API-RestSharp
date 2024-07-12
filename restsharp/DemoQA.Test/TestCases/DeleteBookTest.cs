using Core.Reports;
using DemoQA.Service.Model.Response;
using DemoQA.Service.Services;
using DemoQA.Test.DataProvider;
using FluentAssertions;
using Newtonsoft.Json;


namespace DemoQA.Test.TestCases
{
    [TestFixture, Category("DeleteBooks")]
    public class DeleteBookTest : BaseTest
    {
        private BookServices _bookServices;
        private UserProvider _userProvider;

        public DeleteBookTest()
        {
            _bookServices = new BookServices(ApiClient);
        }
        [Test]
        [TestCase("user_01", "book1")]
        public async Task DelteBookSuccesfully(string userInfokeyData, string bookInfoData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);



            ReportLog.Info("1.Add book into collection");
            var response = await _bookServices.AddBookAsync(userInfo.UserId, bookInfo.isbn, token);
            var responseData = response.Data;
            Console.WriteLine(response.Content);

            ReportLog.Info("2.Delete initial book by request");
            var responseDelte = await _bookServices.DeleteBookAsync(userInfo.UserId, bookInfo.isbn, userInfo.Token);
            Console.WriteLine(responseDelte.Content);

            ReportLog.Info("3.Verify status code repsonse");
            responseDelte.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        }
        [Test]
        [TestCase("user_01", "book1")]

        public async Task DeleteBookFromCollectionWithoutAuthotized(string userInfokeyData, string bookInfoData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);

            ReportLog.Info("1.Delete initial book");
            var response = await _bookServices.DeleteBookAsync(userInfo.UserId, bookInfo.isbn, null);
            Console.WriteLine(response.Content);

            ReportLog.Info("2.Verify status code repsonse");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);

            ReportLog.Info("3.Assert delete book repsonse");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((int)result["code"]).Should().Be(1200);
            ((string)result["message"]).Should().Be("User not authorized!");
        }
        [Test]
        [TestCase("user_01", "book1")]

        public async Task DeleteBookFromCollectionWithWrongUserId(string userInfokeyData, string bookInfoData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);


            ReportLog.Info("1.Add book into collection");
            await _bookServices.AddBookAsync(userInfo.UserId, bookInfo.isbn, token);


            ReportLog.Info("2.Delete initial book by request");
            var response = await _bookServices.DeleteBookAsync(userInfo.UserId + "78878", bookInfo.isbn, token);


            ReportLog.Info("3.Verify status code repsonse");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            
            ReportLog.Info("4.Assert delete book repsonse");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(result);
            ((int)result["code"]).Should().Be(1207);
            ((string)result["message"]).Should().Be("User Id not correct!");

          
        }
        [Test]
        [TestCase("user_01", "book1")]

        public async Task DeleteBookFromCollectionWithWrongBookId(string userInfokeyData, string bookInfoData)
        {

            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);

            ReportLog.Info("1.Add book into collection");
            await _bookServices.AddBookAsync(userInfo.UserId, bookInfo.isbn, token);

            ReportLog.Info("2.Delete initial book by request");
            var response = await _bookServices.DeleteBookAsync(userInfo.UserId, bookInfo.isbn + "111", token);


            ReportLog.Info("3.Verify status code repsonse");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            
            
            ReportLog.Info("4.Assert delete book repsonse");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(result);
            ((int)result["code"]).Should().Be(1206);
            ((string)result["message"]).Should().Be("ISBN supplied is not available in User's Collection!");

         
        }
    }
}
