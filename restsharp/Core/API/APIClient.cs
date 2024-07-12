using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using RestSharp.Serializers.NewtonsoftJson;

namespace Core.API
{
    public class APIClient
    {
        private readonly RestClient _client;
        public RestRequest Request;
        private RestClientOptions _requestOptions;

        public APIClient(RestClient client)
        {
            _client = client;
            Request = new RestRequest();
        }

        public APIClient(string url)
        {
            var options = new RestClientOptions(url);
            _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
            Request = new RestRequest();
        }

        public APIClient(RestClientOptions options)
        {
            _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
            Request = new RestRequest();
        }

        public APIClient SetBasicAuthentication(string username, string password)
        {
            _requestOptions.Authenticator = new HttpBasicAuthenticator(username, password);
            return new APIClient(_requestOptions);
        }

        public APIClient SetRequestTokenAuthentication(string consumerKey, string consumerSecret)
        {
            _requestOptions.Authenticator = OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret);
            return new APIClient(_requestOptions);
        }

        public APIClient SetAccessTokenAuthentication(string consumerKey, string consumerSecret, string oauthToken, string oauthTokenSecret)
        {
            _requestOptions.Authenticator = OAuth1Authenticator.ForAccessToken(
                consumerKey, consumerSecret, oauthToken, oauthTokenSecret);
            return new APIClient(_requestOptions);    
        }

        public APIClient SetRequestHeaderAuthentication(string token, string authType = "Bearer")
        {
            _requestOptions.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, authType);
            return new APIClient(_requestOptions);
        }

        public APIClient SetJwtAuthenticator(string token)
        {
            _requestOptions.Authenticator = new JwtAuthenticator(token);
            return new APIClient(_requestOptions);
        }

        public APIClient ClearAuthenticator()
        {
            _requestOptions.Authenticator = null;
            return new APIClient(_requestOptions);
        }


        public APIClient AddDefaultHeader(Dictionary<string, string> headers)
        {
            _client.AddDefaultHeaders(headers);
            return this;
        }

        public APIClient AddHeader(string name, string value)
        {
            Request.AddHeader(name, value);
            return this;
        }

        public APIClient CreateRequest(string source="")
        {
            Request = new RestRequest(source);
            return this;
        }

        public APIClient AddAuthorizationHeader(string value)
        {
            return AddHeader("Authorization", value);
        }

        public APIClient AddHeaderBearerToken(string token)
        {
           return AddHeader("Authorization", $"Bearer {token}");
            // return this;
        }

        public APIClient AddContentTypeHeader(string value)
        {
            return AddHeader("Content-Type", value);
        }

        public APIClient AddParameter(string name, string value)
        {
            Request.AddParameter(name, value);
            return this;
        }

        public APIClient AddBody(object obj, string contentType=null)
        {
            // string json = JsonConvert.SerializeObject(obj);
            Request.AddJsonBody(obj, contentType);
            return this;
        }

        public async Task<RestResponse> ExecuteGetAsync()
        {
            return await _client.ExecuteGetAsync(Request);
        }

        public async Task<RestResponse<T>> ExecuteGetAsync<T>()
        {
            return await _client.ExecuteGetAsync<T>(Request);
        } 

        public async Task<RestResponse> ExecutePostAsync()
        {
            return await _client.ExecutePostAsync(Request);
        }

        public async Task<RestResponse<T>> ExecutePostAsync<T>()
        {
            return await _client.ExecutePostAsync<T>(Request);
        }

        public async Task<RestResponse> ExecuteDeleteAsync()
        {
            Request.Method = Method.Delete;
            return await _client.ExecuteAsync(Request);
        }

        public RestResponse ExecuteDelete()
        {
            Request.Method = Method.Delete;
            return _client.Execute(Request);
        }
        //public async Task<RestResponse> ExecuteDeleteAsync()
        //{
        //    return await _client.ExecuteDeleteAsync(Request);
        //}
        public async Task<RestResponse> ExecutePutAsync()
        {
            return await _client.ExecutePutAsync(Request);
        }

        public async Task<RestResponse<T>> ExecutePutAsync<T>()
        {
            return await _client.ExecutePutAsync<T>(Request);
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>()
        {
            return await _client.ExecuteAsync<T>(Request);
        }
    }
}