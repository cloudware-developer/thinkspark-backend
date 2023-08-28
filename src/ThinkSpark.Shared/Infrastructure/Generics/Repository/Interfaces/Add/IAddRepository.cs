using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Add
{
    public interface IAddRepository<TEntity> where TEntity : class
    {
        Task AdicionarAsync(TEntity entity);
        Task AdicionarAsync(List<TEntity> collection);
    }
}
