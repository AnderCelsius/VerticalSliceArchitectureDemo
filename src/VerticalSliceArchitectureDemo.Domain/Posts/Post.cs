using VerticalSliceArchitectureDemo.Domain.Abstractions;
using VerticalSliceArchitectureDemo.Domain.Blogs;

namespace VerticalSliceArchitectureDemo.Domain.Posts;

public class Post : BaseEntity
{
  public string Title { get; private set; }
  public string Content { get; private set; }
  public Guid BlogId { get; private set; }
  public Blog Blog { get; private set; }

  private Post(Guid id) : base(id)
  { }

  public Post(Guid id, string title, string content, Blog blog) : base(id)
  {
    Title = title;
    Content = content;
    BlogId = blog.Id;
    Blog = blog;
  }
}