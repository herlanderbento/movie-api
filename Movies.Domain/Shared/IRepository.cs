namespace Movies.Domain.Shared;

public interface IRepository<TAggregate> where TAggregate: AggregateRoot
{
    public Task Insert(TAggregate aggregate, CancellationToken cancellationToken);
    public Task<TAggregate> GetById(Guid id, CancellationToken cancellationToken);
    public Task Delete(TAggregate aggregate, CancellationToken cancellationToken);
    public Task Update(TAggregate aggregate, CancellationToken cancellationToken);
}