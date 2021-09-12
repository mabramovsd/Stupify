using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Stupify;
using Stupify.Model;

namespace StupifyTest
{
    public class BaseTestServerFixture
    {
        public TestServer TestServer { get; }
        public ApplicationContext DbContext { get; }
        public HttpClient Client { get; }

        public BaseTestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
            DbContext = TestServer.Host.Services.GetService<ApplicationContext>();
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}