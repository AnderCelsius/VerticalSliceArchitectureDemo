namespace VerticalSliceArchitectureDemo.API.Configurations;

public record DatabaseConfiguration
{
  public string? ConnectionString { get; init; }
}
