using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using ThinkSpark.Shared.Extensions.Common;
using ThinkSpark.Shared.Infrastructure.Exceptions;
using ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Base;
using ThinkSpark.Shared.Infrastructure.Generics.Repository;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Base;
using ThinkSpark.Shared.Infrastructure.Models;
using ThinkSpark.Shared.Infrastructure.Models.Enums;
using ThinkSpark.Shared.Infrastructure.Models.Paged;

namespace ThinkSpark.Shared.Infrastructure.Generics.Application
{
    public class ApplicationBase<TEntity, TEntityVm> : IApplicationBase<TEntity, TEntityVm> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;
        private readonly IDbContext _dbContext;

        public ApplicationBase(IDbContext dbContext)
        {
            _dbContext = dbContext;
            _repository = new RepositoryBase<TEntity>(_dbContext);
        }

        public virtual async Task AdicionarAsync(TEntityVm model)
        {
            try
            {
                TEntity entity = model.ToModelView<TEntity, TEntityVm>();
                await _repository.AdicionarAsync(entity);
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(AdicionarAsync));
            }
        }

        public virtual async Task AdicionarAsync(List<TEntityVm> collection)
        {
            try
            {
                var entities = collection.ToModelView<TEntity, TEntityVm>();
                await _repository.AdicionarAsync(entities);
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(AdicionarAsync));
            }
        }

        public virtual async Task AtualizarAsync(TEntityVm model)
        {
            try
            {
                TEntity entity = model.ToModelView<TEntity, TEntityVm>();
                await _repository.AtualizarAsync(entity);
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(AtualizarAsync));
            }
        }

        public virtual async Task AtualizarAsync(List<TEntityVm> collection)
        {
            try
            {
                var entities = collection.ToModelView<TEntity, TEntityVm>();
                await _repository.AtualizarAsync(entities);
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(AtualizarAsync));
            }
        }

        public virtual async Task DeletarAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                await _repository.DeletarAsync(predicate);
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(DeletarAsync));
            }
        }

        public virtual async Task<int> ObterTotalPorFiltroAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _repository.ObterTotalPorFiltroAsync(predicate);
                return result;
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(ObterTotalPorFiltroAsync));
            }
        }

        public virtual async Task<TEntity?> ObterItemPorFiltroAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _repository.ObterItemPorFiltroAsync(predicate);
                return result;
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(ObterItemPorFiltroAsync));
            }
        }

        public virtual async Task<List<TEntity>> ObterListaPorFiltroAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _repository.ObterListaPorFiltroAsync(predicate);
                return result;
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(ObterListaPorFiltroAsync));
            }
        }

        public virtual async Task<List<SelectItem>> ObterComboAsync(Expression<Func<TEntity, bool>>? predicate = null, string propertyNameDescribe = "")
        {
            try
            {
                var result = await _repository.ObtemComboAsync(predicate, propertyNameDescribe);
                return result;
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(ObterListaPorFiltroAsync));
            }
        }

        public virtual async Task<bool> VerificaExistenciaAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _repository.VerificaExistenciaAsync(predicate);
                return result;
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(VerificaExistenciaAsync));
            }
        }

        public virtual Paged<List<TEntityVm>> CalculaPaginacao<TKey>(int? currentPage, int? itemsPerPage, IQueryable<TEntity> collection, Func<TEntity, TKey> orderByColumn, OrderByDirectionEnum? orderByDirection = OrderByDirectionEnum.Asc)
        {
            Paged<List<TEntityVm>> pagination = new Paged<List<TEntityVm>>();
                       
            pagination.OrderByDirection = orderByDirection ?? OrderByDirectionEnum.Asc;
            pagination.CurrentPage = currentPage ?? 1;
            pagination.TotalItems = collection.Count();
            pagination.TotalPages = (int)Math.Ceiling((double)collection.Count() / (int)itemsPerPage!);
            pagination.ItemsPerPage = (int)itemsPerPage;

            bool hasPreviousPage = currentPage > 1;
            bool hasNextPage = currentPage < pagination.TotalPages;
            bool hasFirstPage = currentPage > 1;
            bool hasLastPage = currentPage < pagination.TotalPages;

            pagination.CurrentPageUrl = $"?currentPage={pagination.CurrentPage}?itemsPerPage={itemsPerPage}";

            if (hasFirstPage)
                pagination.FirstPageUrl = $"?currentPage={1}?itemsPerPage={itemsPerPage}";

            if (hasPreviousPage)
                pagination.PreviousPageUrl = $"?currentPage={(pagination.CurrentPage > 1 ? pagination.CurrentPage - 1 : 1)}?itemsPerPage={itemsPerPage}";

            if (hasNextPage)
                pagination.NextPageUrl = $"?currentPage={(currentPage < pagination.TotalPages ? currentPage + 1 : string.Empty)}?itemsPerPage={itemsPerPage}";

            if (hasLastPage)
                pagination.LastPageUrl = $"?currentPage={pagination.TotalPages}?itemsPerPage={itemsPerPage}";

            collection = collection.Skip((int)((currentPage! - 1) * itemsPerPage!)).Take((int)itemsPerPage!);

            if (orderByDirection == OrderByDirectionEnum.Asc)
                pagination.Collection = collection.OrderBy(orderByColumn).ToList().ToViewModel<List<TEntity>, List<TEntityVm>>();
            else if (orderByDirection == OrderByDirectionEnum.Desc)
                pagination.Collection = collection.OrderByDescending(orderByColumn).ToList().ToViewModel<List<TEntity>, List<TEntityVm>>();

            return pagination;
        }
    }
}
