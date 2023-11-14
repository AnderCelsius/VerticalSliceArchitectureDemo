using System.Reflection;
using System.Text.Json.Serialization;
using VerticalSliceArchitectureDemo.API.Configurations;
using VerticalSliceArchitectureDemo.API.Extensions;
using VerticalSliceArchitectureDemo.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(opts =>
{
  opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
  opts.JsonSerializerOptions.DefaultIgnoreCondition =
    JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddDatabase<AppDbContext>(builder.Configuration.GetRequiredSection("Database").Get<DatabaseConfiguration>());

builder.Services.AddHealthChecks()
  .AddDbContextCheck<AppDbContext>();

builder.Services.AddMediatR(c =>
  c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  await app.Services.MigrateDatabaseToLatestVersion<AppDbContext>();
  app.UseSwagger().UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
