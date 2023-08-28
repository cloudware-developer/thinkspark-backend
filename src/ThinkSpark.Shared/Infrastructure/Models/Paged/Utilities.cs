using System.Linq.Expressions;
using ThinkSpark.Shared.Infrastructure.Models.Paged.Interfaces;

namespace ThinkSpark.Shared.Infrastructure.Models.Paged
{
    public static class Utilities<TEntity>
    {
        /// <summary>
        /// Adiciona a ordenação de um campo da entidade em uma consulta
        /// </summary>
        /// <param name="query">Consulta</param>
        /// <param name="sort">informações de ordenação dos campos</param>
        /// <param name="sortItemIteract">Ação que será executada para campo a ser ordenado</param>
        public static List<T> Sort<T>(List<T> query, ISortable sort, Func<SortField, Expression<Func<T, object>>> sortItemIteract)
        {
            if (sort.SortField != null)
            {
                var exp = sortItemIteract(sort.SortField);
                if (exp != null)
                    query = sort.SortField.IsDescending ?? false ? query.AsQueryable().OrderByDescending(exp).ToList() : query.AsQueryable().OrderBy(exp).ToList();
            }

            return query;
        }

        /// <summary>
        /// Realiza a paginação de uma consulta
        /// </summary>
        /// <param name="query">Consulta a ser paginada</param>
        /// <param name="paginationInfo">Informações de paginação</param>
        public static List<T> Paginate<T>(List<T> query, IPaginable paginationInfo)
        {
            Pagination pagination = paginationInfo.Pagination;

            if (pagination.IsValid())
            {
                var result = query.Skip(pagination.ItemStartIndex).Take(pagination.PageSize).ToList();
                return result;
            }

            return query;
        }
    }

}
