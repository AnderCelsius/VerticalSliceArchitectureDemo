using VerticalSliceArchitectureDemo.Domain.Abstractions;
using VerticalSliceArchitectureDemo.Domain.Posts;

namespace VerticalSliceArchitectureDemo.Domain.Blogs;

public sealed class Blog : BaseEntity
{
    private Blog(Guid id) : base(id) { }
    private Blog(Guid id, Name name, string description)
      : base(id)
    {
        Name = name;
        Description = description;
        Posts = new HashSet<Post>();
    }

    public Name Name { get; private set; }
    public string Description { get; private set; }
    public ICollection<Post> Posts { get; private set; }

    public static Blog Create(Name name, string description = "")
    {
        var blog = new Blog(Guid.NewGuid(), name, description);

        blog.Raise(new BlogCreatedDomainEvent(blog.Id));

        return blog;
    }
}