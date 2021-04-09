using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Guitars.Pact.Consumer.Tests
{
    public class MockProviderClient
    {
        private readonly HttpClient _client;

        public MockProviderClient(string baseUri)
        {
            _client = new HttpClient { BaseAddress = new Uri(baseUri) };
        }

        public T SendGenericRequest<T>(HttpMethod httpMethod, string path, HttpStatusCode expectedStatusCode, object requestBody = null, object responseBody = null)
        {
            var request = new HttpRequestMessage(httpMethod, path);

            request.Headers.Add("Accept", "application/json");

            if (requestBody != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            }

            var response = _client.SendAsync(request);

            if (responseBody != null)
            {
                response.Result.Content = new StringContent(JsonConvert.SerializeObject(responseBody), Encoding.UTF8, "application/json");
            }

            var content = response.Result.Content.ReadAsStringAsync().Result;
            var status = response.Result.StatusCode;
            var reasonPhrase = response.Result.ReasonPhrase;

            request.Dispose();
            response.Dispose();

            if (status == expectedStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(content);
            }

            throw new Exception(reasonPhrase);
        }
    }
}