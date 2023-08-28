using ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Add;
using ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Delete;
using ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Get;
using ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Update;

namespace ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Base
{
    public interface IApplicationBase<TEntity, TEntityVm> :
     IAddApplication<TEntity, TEntityVm>,
     IDeleteApplication<TEntity>,
     IGetApplication<TEntity>,
     IUpdateApplication<TEntity, TEntityVm> where TEntity : class
    {
    }
}
