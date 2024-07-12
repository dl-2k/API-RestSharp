using Core.Reports;
using DemoQA.Service.Services;
using DemoQA.Test.DataProvider;
using FluentAssertions;
using Newtonsoft.Json;


namespace DemoQA.Test.TestCases
{
    [TestFixture, Category("GetUser")]
    public class GetUserTest : BaseTest
    {
        private UserServices _userServices;
        private UserProvider _userProvider; 

        public GetUserTest()
        {
            _userServices = new UserServices(ApiClient);
            _userProvider = new UserProvider();
        }
        [Test]
        [TestCase("user_01")]
        public async Task GetUserSuccessfully(string userInfokeyData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfokeyData);
            _userProvider.StoreUserToken(userInfokeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfokeyData);

            ReportLog.Info("1.Get user by User ID");
            var response = await _userServices.GetUserAsync(userInfo.UserId,token);
            
            ReportLog.Info("2.Verify status code repsonse");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content);
            
            
            ReportLog.Info("3.Assert get user repsonse");
            response.Data.UserId.Should().Be(userInfo.UserId);
            response.Data.Username.Should().BeEquivalentTo(userInfo.Username);
        }
        [Test]
        [TestCase("user_01")]

        public async Task GetUserDetailWithoutAuthorized(string userInfoKeyData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfoKeyData);

            ReportLog.Info("1.Get user by User ID");
            var response = await _userServices.GetUserAsync(userInfo.UserId, null);

            ReportLog.Info("2.Verify status code repsonse");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);

            ReportLog.Info("3.Assert get user repsonse");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((int)result["code"]).Should().Be(1200);
            ((string)result["message"]).Should().Be("User not authorized!");
        }
        [Test]
        [TestCase("user_01")]

        public async Task GetUserDetailWithWrongUserId(string userInfoKeyData)
        {
            var userInfo = UserProvider.GetUserInfoData(userInfoKeyData);
            _userProvider.StoreUserToken(userInfoKeyData, userInfo);
            string token = _userProvider.GetUserToken(userInfoKeyData);

            ReportLog.Info("1.Get user by User ID");
            var response = await _userServices.GetUserAsync(userInfo.UserId + "14785",  token );


            ReportLog.Info("2.Verify status code repsonse");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            
            ReportLog.Info("3.Assert get user repsonse");
            var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
            ((int)result["code"]).Should().Be(1207);
            ((string)result["message"]).Should().Be("User not found!");
        }
    }
}