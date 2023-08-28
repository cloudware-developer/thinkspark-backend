using System.Linq.Expressions;
using ThinkSpark.Shared.Infrastructure.Models;

namespace ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Get
{
    public interface IGetApplication<TEntity> where TEntity : class
    {
        Task<TEntity?> ObterItemPorFiltroAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> ObterListaPorFiltroAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> ObterTotalPorFiltroAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> VerificaExistenciaAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<SelectItem>> ObterComboAsync(Expression<Func<TEntity, bool>>? predicate = null, string propertyNameDescribe = "");
    }
}
