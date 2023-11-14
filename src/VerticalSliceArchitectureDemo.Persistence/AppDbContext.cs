using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VerticalSliceArchitectureDemo.Domain.Blogs;
using VerticalSliceArchitectureDemo.Domain.Posts;

namespace VerticalSliceArchitectureDemo.Persistence;

public class AppDbContext : DbContext
{
  public DbSet<Blog> Blogs { get; set; }
  public DbSet<Post> Posts { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Apply configurations from separate configuration classes
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(modelBuilder);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    // logic to handle domain events before saving changes

    var result = await base.SaveChangesAsync(cancellationToken);

    // logic to handle domain events after saving changes

    return result;
  }
}