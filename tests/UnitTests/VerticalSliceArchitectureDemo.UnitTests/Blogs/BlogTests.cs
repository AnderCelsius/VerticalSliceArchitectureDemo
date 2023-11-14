using FluentAssertions;
using VerticalSliceArchitectureDemo.Domain.Blogs;

namespace VerticalSliceArchitectureDemo.UnitTests.Blogs;

public sealed class BlogTests
{
  [Fact]
  public void Create_Should_ReturnBlog_WhenNameIsValid()
  {
    // Arrange
    var blog = CreateTestBlog();

    // Assert
    blog.Should().NotBeNull();
  }

  [Fact]
  public void Create_Should_RaiseDomainEvent_WhenNameIsValid()
  {
    // Arrange
    var blog = CreateTestBlog();

    // Assert
    blog.DomainEvents.Should().ContainSingle()
      .Which.Should().BeOfType<BlogCreatedDomainEvent>();
  }

  private static Blog CreateTestBlog()
  {
    var name = new Name("Code with Obai");

    return Blog.Create(name, "Teaching key topic in .Net");
  }
}
