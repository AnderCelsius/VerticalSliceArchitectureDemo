using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitectureDemo.API.Exceptions;
using VerticalSliceArchitectureDemo.Domain.Blogs;
using VerticalSliceArchitectureDemo.Persistence;

namespace VerticalSliceArchitectureDemo.API.Features;

public static class GetById
{
  public record Request(Guid BlogId) : IRequest<Response>;

  public record Response(Guid Id, string Name, string Description);

  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Blog, Response>().ForCtorParam(nameof(Response.Name),
        op => op.MapFrom(blog => blog.Name.Value));
    }
  }

  public class Handler : IRequestHandler<Request, Response>
  {
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public Handler(AppDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }


    public async Task<Response> Handle(Request request,
      CancellationToken cancellationToken)
    {
      var blog = await _context.Blogs.SingleOrDefaultAsync(
        blog => blog.Id == request.BlogId, cancellationToken);

      if (blog is null)
      {
        throw new BlogNotFoundException(
          $"Could not find an active blog with ID {request.BlogId}.");
      }

      return _mapper.Map<Response>(blog);
    }
  }
}