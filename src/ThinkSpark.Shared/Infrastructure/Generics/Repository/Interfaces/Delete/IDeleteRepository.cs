using System.Linq.Expressions;

namespace ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Delete
{
    public interface IDeleteRepository<TEntity> where TEntity : class
    {
        Task DeletarAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
