using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace Guitars.Pact.Provider.Tests
{
    public class ProviderStateMiddleware
    {
        private const string ConsumerName = "GuitarsConsumer";
        private readonly RequestDelegate _next;
        private readonly IDictionary<string, Action> _providerStates;

        public ProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
            _providerStates = new Dictionary<string, Action>
            {
                {
                    "A GET request to retrieve guitars and match data",
                    DoNothing
                },
                {
                    "A GET request to retrieve guitars and match model",
                    DoNothing
                },
                {
                    "A POST request to create a new guitar and match data",
                    DoNothing
                },
                {
                    "A POST request to create a new guitar and match model",
                    DoNothing
                },
                {
                    "A PUT request to update a guitar and match data",
                    DoNothing
                },
                {
                    "A PUT request to update a guitar and match model",
                    DoNothing
                },
                {
                    "A DELETE request to delete a guitar",
                    DoNothing
                }
            };
        }

        private void DoSomething()
        {
            Console.WriteLine("Doing Something!!!");
        }

        private void DoNothing()
        {
            Console.WriteLine("Doing Nothing!!!");
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                HandleProviderStatesRequest(context);
            }

            else
            {
                await _next(context);
            }
        }

        private void HandleProviderStatesRequest(HttpContext context)
        {
            try
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() && context.Request.Body != null)
                {
                    string jsonRequestBody;

                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                    {
                        jsonRequestBody = reader.ReadToEnd();
                    }

                    var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                    //A null or empty provider state key must be handled
                    if (providerState != null && !string.IsNullOrEmpty(providerState.State) && providerState.Consumer == ConsumerName)
                    {
                        _providerStates[providerState.State].Invoke();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex}");
            }
        }
    }
}
