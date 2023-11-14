using Microsoft.Extensions.DependencyInjection;
using System.Net;
using VerticalSliceArchitectureDemo.API.Features;
using VerticalSliceArchitectureDemo.Domain.Blogs;
using VerticalSliceArchitectureDemo.IntegrationTests.Fixtures;
using VerticalSliceArchitectureDemo.IntegrationTests.Helpers;
using VerticalSliceArchitectureDemo.Persistence;

namespace VerticalSliceArchitectureDemo.IntegrationTests.Blogs;

[Collection("test collection")]
public sealed class GetByIdTests : IDisposable
{
  private readonly TestFixture _fixture;

  private readonly IServiceScope _serviceScope;

  private readonly AppDbContext _context;

  public GetByIdTests()
  {
    _fixture = new TestFixture();
    _fixture.MockServer.Reset();
    _serviceScope = _fixture.Server.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    _context = _serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
  }

  public void Dispose()
  {
    _serviceScope.Dispose();
    _fixture.Dispose();
    _context.Dispose();
  }

  [Fact]
  public async Task GetBlogById_Returns404_WhenBlogDoesNotExist()
  {
    var response = await _fixture.Client.GetAsync(
      $"/blogs/{Guid.NewGuid()}");

    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }

  [Fact]
  public async Task GetBlogById_ReturnsABlog_WhenItExists()
  {
    var blog = CreateTestBlog();
    _context.Blogs.Add(blog);
    await _context.SaveChangesAsync();

    var response = await _fixture.Client.GetFromJsonFixedAsync<GetById.Response>(
      $"/blogs/{blog.Id}");

    Assert.NotNull(response);
    Assert.Equal(blog.Id, response!.Id);
    Assert.Equal(blog.Name.Value, response.Name);
    Assert.Equal(blog.Description, response.Description);
  }

  private static Blog CreateTestBlog()
  {
    var name = new Name("Code with Obai");

    return Blog.Create(name, "Teaching key topic in .Net");
  }
}
