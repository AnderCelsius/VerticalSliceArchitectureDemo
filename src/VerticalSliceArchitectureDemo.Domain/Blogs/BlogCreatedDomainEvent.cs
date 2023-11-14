using VerticalSliceArchitectureDemo.Domain.Abstractions;

namespace VerticalSliceArchitectureDemo.Domain.Blogs;

public sealed record BlogCreatedDomainEvent(Guid BlogId) : IDomainEvents;