using Core.API;
using DemoQA.Service.Model.Request;
using DemoQA.Service.Model.Response;
using RestSharp;


namespace DemoQA.Service.Services
{
    public class BookServices
    {
        private readonly APIClient _client;

        public BookServices(APIClient client)
        {
            _client = client;
        }
        public async Task<RestResponse<AddBookResponseDto>> AddBookAsync(string userId, string bookId, string token)
        {
            var requestBody = new AddBookRequestDto()
            {
                userId = userId,
                CollectionOfIsbns = new List<CollectionOfIsbn>
                {
                    new CollectionOfIsbn { isbn = bookId }
                }
            };
            return await _client.CreateRequest(String.Format(APIConstant.AddBookToCollectionEndPoint, userId))
                .AddContentTypeHeader("application/json")
                .AddHeader("accept", "application/json")
                .AddHeaderBearerToken(token)
                .AddBody(requestBody)
                .ExecutePostAsync<AddBookResponseDto>();
        }

        public async Task<RestResponse>DeleteBookAsync(string userId, string bookId, string token)
        {
            var requestBody = new DeleteBookResquestDto()
            {
                userId = userId,
                isbn = bookId
            };
            return await _client.CreateRequest(String.Format(APIConstant.DeleteBookFromCollectionEndPoint, userId))
                .AddContentTypeHeader("application/json")
                .AddHeader("accept", "application/json")
                .AddHeaderBearerToken(token)
                .AddBody(requestBody)
                .ExecuteDeleteAsync();
        }

        public async Task<RestResponse<ReplaceBookResponeDto>> ReplaceBookAsync(string userId, string replaceBookId,string bookId, string token)
        {
            var requestBody = new ReplaceBookRequestDto()
            {
                userId = userId,
                isbn = replaceBookId
            };
            return await _client.CreateRequest(String.Format(APIConstant.ReplaceBookFromCollectionEndPoint, bookId))
                .AddContentTypeHeader("application/json")
                .AddHeader("accept", "application/json")
                .AddHeaderBearerToken(token)
                .AddBody(requestBody)
                .ExecutePutAsync<ReplaceBookResponeDto>();
        }
    }
}
