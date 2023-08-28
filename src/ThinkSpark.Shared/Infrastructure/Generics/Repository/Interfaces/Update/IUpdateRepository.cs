namespace ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Update
{
    public interface IUpdateRepository<TEntity> where TEntity : class
    {
        Task AtualizarAsync(TEntity entity);
        Task AtualizarAsync(List<TEntity> collection);
    }
}
