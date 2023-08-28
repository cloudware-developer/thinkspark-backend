using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ThinkSpark.Shared.Extensions.Common;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Base;
using ThinkSpark.Shared.Infrastructure.Models;

namespace ThinkSpark.Shared.Infrastructure.Generics.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly IDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;  

        public RepositoryBase(IDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task AdicionarAsync(TEntity entity)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (entity.HasProperty("CriadoEm"))
                    {
                        var propertyInfo = entity.GetType().GetProperty("CriadoEm");
                        propertyInfo?.SetValue(entity, DateTime.Now);
                    }

                    if (entity.HasProperty("EditadoEm"))
                    {
                        var propertyInfo = entity.GetType().GetProperty("EditadoEm");
                        propertyInfo?.SetValue(entity, null);
                    }

                    entity.ToUpperFields();
                    await _dbSet.AddAsync(entity);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex.ExceptionHandler(nameof(AdicionarAsync));
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public async Task AdicionarAsync(List<TEntity> collection)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var entity in collection)
                    {
                        if (entity.HasProperty("CriadoEm"))
                        {
                            var propertyInfo = entity.GetType().GetProperty("CriadoEm");
                            propertyInfo?.SetValue(entity, DateTime.Now);
                        }

                        if (entity.HasProperty("EditadoEm"))
                        {
                            var propertyInfo = entity.GetType().GetProperty("EditadoEm");
                            propertyInfo?.SetValue(entity, null);
                        }

                        entity.ToUpperFields();
                        await _dbSet.AddAsync(entity);
                    } 

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex.ExceptionHandler(nameof(AdicionarAsync));
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public async Task AtualizarAsync(TEntity entity)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (entity.HasProperty("EditadoEm"))
                    {
                        var propertyInfo = entity.GetType().GetProperty("EditadoEm");
                        propertyInfo?.SetValue(entity, DateTime.Now);
                    }

                    entity.ToUpperFields();

                    _dbContext.Entry(entity).State = EntityState.Modified;

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex.ExceptionHandler(nameof(AtualizarAsync));
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public async Task AtualizarAsync(List<TEntity> collection)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var entity in collection)
                    {
                        if (entity.HasProperty("EditadoEm"))
                        {
                            var propertyInfo = entity.GetType().GetProperty("EditadoEm");
                            propertyInfo?.SetValue(entity, DateTime.Now);
                        }

                        _dbContext.Entry(entity).State = EntityState.Modified;
                    }

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex.ExceptionHandler(nameof(AtualizarAsync));
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public async Task DeletarAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var collection = await _dbSet.Where(predicate).ToListAsync();

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var entity in collection)
                        _dbSet.Remove(entity);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex.ExceptionHandler(nameof(DeletarAsync));
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public async Task<int> ObterTotalPorFiltroAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                List<TEntity> collection = new List<TEntity>();

                collection = await _dbSet.IgnoreAutoIncludes().Where(predicate).ToListAsync();
                var count = collection.Count;

                return count;
            }
            catch (Exception ex)
            {
                throw ex.ExceptionHandler(nameof(ObterTotalPorFiltroAsync));
            }
        }

        public async Task<TEntity?> ObterItemPorFiltroAsync(Expression<Func<TEntity, bool>>? predicate, bool incluirEntidades = false)
        {
            try
            {
                TEntity? entity;

                IQueryable<TEntity> query = (incluirEntidades) ? _dbSet.AsQueryable() : _dbSet.IgnoreAutoIncludes().AsQueryable();

                if (predicate == null)
                    entity = await query.FirstOrDefaultAsync();
                else
                    entity = await query.Where(predicate).FirstOrDefaultAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex.ExceptionHandler(nameof(ObterItemPorFiltroAsync));
            }
        }

        public async Task<List<TEntity>> ObterListaPorFiltroAsync(Expression<Func<TEntity, bool>>? predicate = null, bool incluirEntidades = false)
        {
            try
            {
                List<TEntity> collection = new List<TEntity>();

                IQueryable<TEntity> query = (incluirEntidades) ? _dbSet.AsQueryable() : _dbSet.IgnoreAutoIncludes().AsQueryable();

                if (predicate == null)
                    collection = await query.ToListAsync();
                else
                    collection = await query.Where(predicate).ToListAsync();

                return collection;
            }
            catch (Exception ex)
            {
                throw ex.ExceptionHandler(nameof(ObterListaPorFiltroAsync));
            }
        }

        public IQueryable<TEntity> ObterListaPorFiltroQueryable(Expression<Func<TEntity, bool>>? predicate = null, bool incluirEntidades = false)
        {
            try
            {
                IQueryable<TEntity> collection = (incluirEntidades) ? _dbSet.AsQueryable() : _dbSet.IgnoreAutoIncludes().AsQueryable();

                if (predicate == null)
                    return collection;
                else
                    return collection.Where(predicate);
            }
            catch (Exception ex)
            {
                throw ex.ExceptionHandler(nameof(ObterListaPorFiltroQueryable));
            }
        }

        public Task<bool> VerificaExistenciaAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var isExists = _dbSet.IgnoreAutoIncludes().Any(predicate);
                return Task.FromResult(isExists);
            }
            catch (Exception ex)
            {
                throw ex.ExceptionHandler(nameof(VerificaExistenciaAsync));
            }
        }

        public async Task<List<SelectItem>> ObtemComboAsync(Expression<Func<TEntity, bool>>? predicate = null, string propertyNameDescribe = "")
        {
            List<TEntity> entities = new List<TEntity>();

            if (predicate == null)
                entities = await _dbSet.IgnoreAutoIncludes().ToListAsync();
            else
                entities = await _dbSet.IgnoreAutoIncludes().Where(predicate).ToListAsync();

            var collection = new List<SelectItem>();

            if (entities.Any())
            {
                var count = 1;

                foreach (var item in entities)
                {
                    var nameObj = item.GetType();
                    var propertyNameId = $"{nameObj.Name}Id";
                    var propertyInfoId = nameObj.GetProperty(propertyNameId);
                    var propertyValueId = propertyInfoId?.GetValue(item);

                    var propertyName = $"Nome";
                    if (!string.IsNullOrEmpty(propertyNameDescribe))
                        propertyName = $"{propertyNameDescribe}";

                    var propertyInfoName = nameObj.GetProperty(propertyName);
                    var propertyValueName = (string)(propertyInfoName?.GetValue(item) ?? string.Empty);

                    collection.Add(new SelectItem(count++, propertyValueName, propertyValueId));
                }
            }

            return collection;
        }
    }
}
