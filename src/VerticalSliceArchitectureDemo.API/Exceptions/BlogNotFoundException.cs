namespace VerticalSliceArchitectureDemo.API.Exceptions;

public class BlogNotFoundException : Exception
{
  public BlogNotFoundException(string message) : base(message)
  {
  }
}