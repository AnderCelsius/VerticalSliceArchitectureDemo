using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VerticalSliceArchitectureDemo.Persistence;
using WireMock.Server;

namespace VerticalSliceArchitectureDemo.IntegrationTests.Fixtures;

public class TestFixture : IDisposable
{

  private readonly WebApplicationFactory<Program> _applicationFactory;

  public TestFixture(IEnumerable<KeyValuePair<string, string>>? configurationOverride = null)
  {
    // Initialize SQLite provider here
    SQLitePCL.Batteries.Init();

    MockServer = WireMockServer.Start();

    var configBuilder = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json");

    configurationOverride ??= new List<KeyValuePair<string, string>>();

    configBuilder.AddInMemoryCollection(configurationOverride);

    var config = configBuilder.Build();

#pragma warning disable CA2000
    _applicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => builder.UseConfiguration(config));
#pragma warning restore CA2000

    Server = _applicationFactory.Server;
    Client = _applicationFactory.CreateClient();

    var dbContextFactory = Server.Services
      .GetRequiredService<IDbContextFactory<AppDbContext>>();
    using var dbContext = dbContextFactory.CreateDbContext();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.Migrate();
  }

  public TestServer Server { get; }

  public HttpClient Client { get; }

  public WireMockServer MockServer { get; }

  public void Dispose()
  {
    MockServer.Dispose();
    var dbContextFactory = Server.Services
      .GetRequiredService<IDbContextFactory<AppDbContext>>();
    using var dbContext = dbContextFactory.CreateDbContext();
    dbContext.Database.EnsureDeleted();

    _applicationFactory.Dispose();
  }
}
