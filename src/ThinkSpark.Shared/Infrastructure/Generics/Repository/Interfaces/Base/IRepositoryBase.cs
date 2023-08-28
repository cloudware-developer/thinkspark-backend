using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Add;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Delete;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Get;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Update;

namespace ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Base
{
    public interface IRepositoryBase<TEntity> :
        IAddRepository<TEntity>,
        IDeleteRepository<TEntity>,
        IGetRepository<TEntity>,
        IUpdateRepository<TEntity> where TEntity : class
    {
    }
}
