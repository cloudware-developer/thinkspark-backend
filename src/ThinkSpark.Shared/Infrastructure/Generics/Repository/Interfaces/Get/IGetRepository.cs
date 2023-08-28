using System.Linq.Expressions;
using ThinkSpark.Shared.Infrastructure.Models;

namespace ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Get
{
    public interface IGetRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> ObterItemPorFiltroAsync(Expression<Func<TEntity, bool>>? predicate, bool incluirEntidades = false);
        Task<List<TEntity>> ObterListaPorFiltroAsync(Expression<Func<TEntity, bool>>? predicate = null, bool incluirEntidades = false);
        IQueryable<TEntity> ObterListaPorFiltroQueryable(Expression<Func<TEntity, bool>>? predicate = null, bool incluirEntidades = false);
        Task<int> ObterTotalPorFiltroAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> VerificaExistenciaAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<SelectItem>> ObtemComboAsync(Expression<Func<TEntity, bool>>? predicate = null, string propertyNameDescribe = "");
    }
}
