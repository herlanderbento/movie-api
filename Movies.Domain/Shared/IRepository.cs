namespace Movies.Domain.Shared;

public interface IRepository<TAggregate> where TAggregate: AggregateRoot
{
    public Task<TAggregate> InsertAsync(TAggregate aggregate);
    public Task<TAggregate?> GetByIdAsync(Guid id);
    public Task<IReadOnlyList<TAggregate>> GetAllAsync();
    public Task<TAggregate> UpdateAsync(TAggregate aggregate);
    public Task DeleteAsync(TAggregate aggregate);
}