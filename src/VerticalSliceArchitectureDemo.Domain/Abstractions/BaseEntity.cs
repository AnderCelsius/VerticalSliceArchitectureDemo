namespace VerticalSliceArchitectureDemo.Domain.Abstractions;

public abstract class BaseEntity
{
  private readonly List<IDomainEvents> _domainEvents = new();
  protected BaseEntity(Guid id)
  {
    Id = id;
  }
  public Guid Id { get; init; }

  public IReadOnlyList<IDomainEvents> DomainEvents => _domainEvents.ToList();

  protected void Raise(IDomainEvents domainEvents)
  {
    _domainEvents.Add(domainEvents);
  }
}