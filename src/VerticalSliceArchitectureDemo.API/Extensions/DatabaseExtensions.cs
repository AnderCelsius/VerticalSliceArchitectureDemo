using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitectureDemo.API.Configurations;
using VerticalSliceArchitectureDemo.API.Helpers;

namespace VerticalSliceArchitectureDemo.API.Extensions;

public static class DatabaseExtensions
{
  public static void AddDatabase<TDbContext>(
    this IServiceCollection serviceCollection,
    DatabaseConfiguration? databaseConfiguration)
    where TDbContext : DbContext
  {
    serviceCollection.AddDbContextFactory<TDbContext>(builder => builder.UseSqlite(databaseConfiguration.ConnectionString));

    serviceCollection.AddTransient<DatabaseMigrator<TDbContext>>();
  }

  public static Task MigrateDatabaseToLatestVersion<TDbContext>(this IServiceProvider serviceProvider)
    where TDbContext : DbContext
  {
    return serviceProvider.GetRequiredService<DatabaseMigrator<TDbContext>>().MigrateDbToLatestVersion();
  }
}
