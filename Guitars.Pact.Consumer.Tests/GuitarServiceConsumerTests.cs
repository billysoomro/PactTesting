using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Guitars.Models;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;
namespace Guitars.Pact.Consumer.Tests
{
    public class GuitarServiceConsumerTests : IClassFixture<GuitarServicePact>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _mockProviderServiceBaseUri;
        private readonly MockProviderClient _mockProviderClient;

        public GuitarServiceConsumerTests(GuitarServicePact guitarServicePact)
        {
            _mockProviderService = guitarServicePact.MockProviderService;
            _mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
            _mockProviderServiceBaseUri = guitarServicePact.MockProviderServiceBaseUri;
            _mockProviderClient = new MockProviderClient(_mockProviderServiceBaseUri);
        }

        [Fact]
        public void Should_Get_Guitars_And_Match_Data()
        {
            var path = "/api/guitars";
            var status = 200;
            var guitars = new List<Guitar>
            {
                new Guitar("PRS" , "Tremonti Signature Model", "Solid body", 6),
                new Guitar("Gibson" , "Les Paul", "Solid body", 6),
                new Guitar("Fender" , "Stratocaster", "Solid body", 6),
                new Guitar("Ibanez" , "RG Series", "Solid body", 7),
                new Guitar("ESP" , "KH-2 VINTAGE", "Solid body", 6)
            };

            //Arrange
            _mockProviderService
                .Given("A GET request to retrieve guitars")
                .UponReceiving("Should receive a list of matching guitars")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = path,
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = status,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = guitars
                }); //NOTE: WillRespondWith call must come last as it will register the interaction

            //Act
            var result = _mockProviderClient.SendGenericRequest<List<Guitar>>(HttpMethod.Get, path, HttpStatusCode.OK, guitars);

            //Assert
            Assert.NotNull(result);
           
            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }

        [Fact]
        public void Should_Get_Guitars_And_Match_Model()
        {
            var path = "/api/guitars";
            var status = 200;
            var guitars = new List<Guitar>
            {
                new Guitar("PRS" , "Tremonti Signature Model", "Solid body", 6),
                new Guitar("Gibson" , "Les Paul", "Solid body", 6),
                new Guitar("Fender" , "Stratocaster", "Solid body", 6),
                new Guitar("Ibanez" , "RG Series", "Solid body", 7),
                new Guitar("ESP" , "KH-2 VINTAGE", "Solid body", 6)
            };

            //Arrange
            _mockProviderService
                .Given("A GET request to retrieve guitars")
                .UponReceiving("Should receive a list of matching guitar objects")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = path,
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = status,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = Match.Type(guitars)
                }); //NOTE: WillRespondWith call must come last as it will register the interaction

            //Act
            var result = _mockProviderClient.SendGenericRequest<List<Guitar>>(HttpMethod.Get, path, HttpStatusCode.OK, guitars);

            //Assert
            Assert.NotNull(result);

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }

        [Fact]
        public void Should_Post_Guitar_And_Match_Data()
        {
            var path = "/api/guitars";
            var status = 201;
            var body = new Guitar("PRS", "Tremonti Signature Model", "Solid body", 6);

            //Arrange
            _mockProviderService
                .Given("A POST request to create a new guitar")
                .UponReceiving("Should receive a matching guitar")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Post,
                    Path = path,
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" },
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = body
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = status,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = body
                }); //NOTE: WillRespondWith call must come last as it will register the interaction

            //Act
            var result = _mockProviderClient.SendGenericRequest<Guitar>(HttpMethod.Post, path, HttpStatusCode.Created, body);

            //Assert
            Assert.NotNull(result);

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }

        [Fact]
        public void Should_Post_Guitar_And_Match_Model()
        {
            var path = "/api/guitars";
            var status = 201;
            var body = new Guitar("PRS", "Tremonti Signature Model", "Solid body", 6);
            
            //Arrange
            _mockProviderService
                .Given("A POST request to create a new guitar")
                .UponReceiving("Should receive a matching guitar object")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Post,
                    Path = path,
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" },
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = body
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = status,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = Match.Type(body)
                }); //NOTE: WillRespondWith call must come last as it will register the interaction

            //Act
            var result = _mockProviderClient.SendGenericRequest<Guitar>(HttpMethod.Post, path, HttpStatusCode.Created, body);

            //Assert
            Assert.NotNull(result);

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }

        [Fact]
        public void Should_Put_Guitar_And_Match_Data()
        {
            var path = "/api/guitars";
            var status = 200;
            var body = new Guitar("PRS", "Tremonti Signature Model", "Solid body", 6);

            //Arrange
            _mockProviderService
                .Given("A PUT request to update a guitar")
                .UponReceiving("Should receive a matching guitar")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Put,
                    Path = path,
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" },
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = body
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = status,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = body
                }); //NOTE: WillRespondWith call must come last as it will register the interaction

            //Act
            var result = _mockProviderClient.SendGenericRequest<Guitar>(HttpMethod.Put, path, HttpStatusCode.OK, body);

            //Assert
            Assert.NotNull(result);

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }

        [Fact]
        public void Should_Put_Guitar_And_Match_Model()
        {
            var path = "/api/guitars";
            var status = 200;
            var body = new Guitar("PRS", "Tremonti Signature Model", "Solid body", 6);

            //Arrange
            _mockProviderService
                .Given("A PUT request to update a guitar")
                .UponReceiving("Should receive a matching updated guitar object")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Put,
                    Path = path,
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" },
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = body
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = status,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = Match.Type(body)
                }); //NOTE: WillRespondWith call must come last as it will register the interaction

            //Act
            var result = _mockProviderClient.SendGenericRequest<Guitar>(HttpMethod.Put, path, HttpStatusCode.OK, body);

            //Assert
            Assert.NotNull(result);

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }
    }
}
