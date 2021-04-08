using System;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace Guitars.Pact.Consumer.Tests
{
    public class GuitarServicePact : IDisposable
    {
        public IPactBuilder PactBuilder { get; }
        public IMockProviderService MockProviderService { get; }
        public int MockServerPort => 9222;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";

        public GuitarServicePact()
        {
            var pactConfig = new PactConfig
            {
                SpecificationVersion = "2.0.0",
                PactDir = @"C:\Pacts\GuitarService\pacts", 
                LogDir = @"C:\Pacts\GuitarService\logs"
            };

            PactBuilder = new PactBuilder(pactConfig);

            PactBuilder.ServiceConsumer("GuitarsConsumer").HasPactWith("GuitarsProvider");

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}
