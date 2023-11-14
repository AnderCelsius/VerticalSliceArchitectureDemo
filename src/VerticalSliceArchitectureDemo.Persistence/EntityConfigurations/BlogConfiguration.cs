using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSliceArchitectureDemo.Domain.Blogs;

namespace VerticalSliceArchitectureDemo.Persistence.EntityConfigurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
  public void Configure(EntityTypeBuilder<Blog> builder)
  {
    // Configure the 'Name' value object as owned type
    builder.OwnsOne(b => b.Name,
      name =>
    {
      // If you're using a column name different than the property name
      name.Property(n => n.Value);
      // Configure it as required (non-nullable)
      name.Property(n => n.Value).IsRequired();
    });

    // Configure the 'Description' property if it exists on the 'Blog' entity
    builder.Property(b => b.Description)
      .IsRequired(); // Only if it should be a required field

    // If you have any other properties or relationships, configure them here
    // e.g., relationships, indexes, property constraints, etc.

    // If the Blog entity contains a collection of Posts, for example, you could configure it like this:
    builder.HasMany(b => b.Posts).WithOne(p => p.Blog).HasForeignKey(p => p.BlogId);
  }
}
