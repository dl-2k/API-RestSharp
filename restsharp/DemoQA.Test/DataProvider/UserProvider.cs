using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ShareData;
using Core.Utilities;
using DemoQA.Test.DataObjects;
using DemoQA.Service.Services;
using DemoQA.Test.DataProvider;
using Newtonsoft.Json;
using FluentAssertions;

namespace DemoQA.Test.DataProvider
{
    public class UserProvider
    {
        private static readonly Dictionary<string, UserDto> _userDto;
        private UserServices _userServices;
       
        static UserProvider()
        {
            _userDto = JsonUtils.ReadDictionaryJson<UserDto>("TestData/Users/user_info.json");
          

        }
        public static UserDto GetUserInfoData(string key)
        {
            if (_userDto.ContainsKey(key))
                return _userDto[key];

            return null;
        }


        public async Task StoreUserToken(string accountKey, UserDto account)
        {

            if (DataStorage.GetData(accountKey) is null)
            {
                var response = await _userServices.GenerateTokenAsync(account.Username, account.Password);
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                var result = (dynamic)JsonConvert.DeserializeObject(response.Content);
                DataStorage.SetData(accountKey, result["token"]);
            }
        }

        public string GetUserToken(string accountKey)
        {
            if (DataStorage.GetData(accountKey) is null)
            {
                throw new Exception("Token is not stored ");
            }

            return DataStorage.GetData(accountKey).ToString();
        }


    }
}