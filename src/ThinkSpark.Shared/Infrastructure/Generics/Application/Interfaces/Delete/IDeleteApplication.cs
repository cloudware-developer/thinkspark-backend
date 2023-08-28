using System.Linq.Expressions;

namespace ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Delete
{
    public interface IDeleteApplication<TEntity> where TEntity : class
    {
        Task DeletarAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
