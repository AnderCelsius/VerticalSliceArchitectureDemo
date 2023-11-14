using VerticalSliceArchitectureDemo.Domain.Abstractions;

namespace VerticalSliceArchitectureDemo.Domain.Posts;

public sealed record PostCreatedDomainEvent(Guid PostId, Guid BlogId) : IDomainEvents;