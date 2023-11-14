using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArchitectureDemo.API.Exceptions;
using VerticalSliceArchitectureDemo.API.Features;

namespace VerticalSliceArchitectureDemo.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class BlogsController : ControllerBase
  {
    private readonly IMediator _mediator;

    public BlogsController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet("{blogId}", Name = "GetBlogById")]
    public async Task<ActionResult<GetById.Response>> GetBlogById(
      [FromRoute] Guid blogId, CancellationToken cancellationToken)
    {
      try
      {
        return await _mediator.Send(new GetById.Request(blogId), cancellationToken);
      }
      catch (BlogNotFoundException e)
      {
        return NotFound(e.Message);
      }
    }
  }
}