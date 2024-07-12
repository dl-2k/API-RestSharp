using Core.Reports;
using DemoQA.Service.Model.Response;
using DemoQA.Service.Services;
using DemoQA.Test.DataProvider;
using FluentAssertions;
using Newtonsoft.Json;


namespace DemoQA.Test.TestCases
{
    [TestFixture, Category("ReplaceBookTest")]
    public class ReplaceBookTest : BaseTest
    {
        private BookServices _bookServices;
        private UserProvider _userProvider;

        public ReplaceBookTest()
        {
            _bookServices = new BookServices(ApiClient);
            _userProvider = new UserProvider();

        }
        [Test]
        [TestCase("user_01", "book1","book2")]
        public async Task ReplaceBookSuccessfully(string userInfokeyData, string bookInfoDataUpdate, string bookInfoData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            var bookInfoUpdate = BookProvider.GetBookInfoData(bookInfoDataUpdate);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);

            
            ReportLog.Info("1.Add book into collection");
            var responseAddBook = await _bookServices.AddBookAsync(userInfo.UserId,bookInfo.isbn,token);

            Console.WriteLine(responseAddBook.Content);
            
            ReportLog.Info("2.Replace initial Add Book of collection");
            var responseReplace = await _bookServices.ReplaceBookAsync(userInfo.UserId, bookInfoUpdate.isbn, bookInfo.isbn, token);
            var responseReplaceData = responseReplace.Data;

            ReportLog.Info("3.Verify status code response");
            responseReplace.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        
            ReportLog.Info("4.Assert replace book response");
            responseReplace.Data.Books.Should().ContainSingle(book => bookInfo.isbn == bookInfoUpdate.isbn);


            ReportLog.Info("5.Delete replace book response");
            _bookServices.DeleteBookAsync(userInfo.UserId, bookInfoUpdate.isbn, token);

        }
        [Test]
        [TestCase("user_01", "book1", "book2")]

        public async Task RepleaceBookInCollectionWithWrongUserId(string userInfokeyData, string bookInfoDataUpdate, string bookInfoData)
        {

            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            var bookInfoUpdate = BookProvider.GetBookInfoData(bookInfoDataUpdate);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);

            ReportLog.Info("1.Add book into collection");

            await _bookServices.AddBookAsync(userInfo.UserId, bookInfo.isbn, token);

            ReportLog.Info("2.Replace initial Add Book of collection");

            var response = await _bookServices.ReplaceBookAsync(userInfo.UserId + "554548", bookInfoUpdate.isbn, bookInfo.isbn, token);

            ReportLog.Info("3.Verify status code response");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);


            ReportLog.Info("4.Assert replace book response");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((int)result["code"]).Should().Be(1207);
            ((string)result["message"]).Should().Be("User Id not correct!");

            ReportLog.Info("5. Delete replaced book in collection");
            await _bookServices.DeleteBookAsync(userInfo.UserId, bookInfoUpdate.isbn, token);
        }
        [Test]
        [TestCase("user_01", "book1", "book2")]

        public async Task ReplaceBookInCollectionWithWrongBookId(string userInfokeyData, string bookInfoDataUpdate, string bookInfoData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            var bookInfoUpdate = BookProvider.GetBookInfoData(bookInfoDataUpdate);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);

            ReportLog.Info("1.Add book into collection");

            await _bookServices.AddBookAsync(userInfo.UserId, bookInfo.isbn, token);

            ReportLog.Info("2.Replace initial Add Book of collection");
            
            var response = await _bookServices.ReplaceBookAsync(userInfo.UserId, bookInfoUpdate.isbn ,bookInfo.isbn + "5454548", token);

            ReportLog.Info("3.Verify status code response");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            ReportLog.Info("4.Assert replace book response");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((int)result["code"]).Should().Be(1206);
            ((string)result["message"]).Should().Be("ISBN supplied is not available in User's Collection!");

            ReportLog.Info("5. Delete replaced book in collection");
            await _bookServices.DeleteBookAsync(userInfo.UserId, bookInfo.isbn, token);
        }
        [Test]
        [TestCase("user_01", "book1", "book2")]

        public async Task ReplaceBookInCollectionWithBookId(string userInfokeyData, string bookInfoDataUpdate, string bookInfoData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            var bookInfoUpdate = BookProvider.GetBookInfoData(bookInfoDataUpdate);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);

            ReportLog.Info("1.Add book into collection");

            await _bookServices.AddBookAsync(userInfo.UserId, bookInfo.isbn, token);

            ReportLog.Info("2.Replace initial Add Book of collection");

            var response = await _bookServices.ReplaceBookAsync(userInfo.UserId, bookInfoUpdate.isbn, bookInfo.isbn, token);

            ReportLog.Info("3.Verify status code response");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            ReportLog.Info("4.Assert replace book response");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((int)result["code"]).Should().Be(1206);
            ((string)result["message"]).Should().Be("ISBN supplied is not available in User's Collection!");

            ReportLog.Info("5. Delete replaced book in collection");
            await _bookServices.DeleteBookAsync(userInfo.UserId, bookInfoUpdate.isbn, token);
        }
        [Test]
        [TestCase("user_01", "book1")]

        public async Task ReplaceBookInCollectionWithBookIdExist(string userInfokeyData, string bookInfoData)
        {

            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            var bookInfo = BookProvider.GetBookInfoData(bookInfoData);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);

            ReportLog.Info("1.Add book into collection");

            await _bookServices.AddBookAsync(userInfo.UserId, bookInfo.isbn, token);

            ReportLog.Info("2.Replace initial Add Book of collection");

            var response = await _bookServices.ReplaceBookAsync(userInfo.UserId, bookInfo.isbn, bookInfo.isbn, token);

            ReportLog.Info("3.Verify status code response");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            ReportLog.Info("4.Assert replace book response");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((int)result["code"]).Should().Be(1206);
            ((string)result["message"]).Should().Be("ISBN supplied is not available in User's Collection!");
            Console.WriteLine(result);

            ReportLog.Info("5. Delete replaced book in collection");
            await _bookServices.DeleteBookAsync(userInfo.UserId, bookInfo.isbn, token);
        }
    }
}
