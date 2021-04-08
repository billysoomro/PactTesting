using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace Guitars.Pact.Provider.Tests
{
    public class ProviderTest : IDisposable
    {
        private readonly IWebHost _webHost;
        private readonly ITestOutputHelper _output;
        private readonly string _selfHostedproviderUrl = "http://localhost:9999";
       
        public ProviderTest(ITestOutputHelper output)
        {
            _output = output;
            _webHost = WebHost.CreateDefaultBuilder()
                .UseUrls(_selfHostedproviderUrl)
                .UseStartup<ProviderTestStartup>()
                .Build();

            _webHost.Start();
        }

        [Fact]
        public void RunProviderVerification()
        {
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput> { new XUnitOutput(_output) },
                Verbose = true,
                ProviderVersion = "1.0.0",
                PublishVerificationResults = true
            };

            new PactVerifier(config)
                .ServiceProvider("GuitarsProvider", _selfHostedproviderUrl)
                .HonoursPactWith("GuitarsConsumer")
                .PactUri(@"C:\Pacts\GuitarService\pacts\guitarsconsumer-guitarsprovider.json")
                .Verify();
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _webHost.StopAsync().GetAwaiter().GetResult();
                    _webHost.Dispose();
                }

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
