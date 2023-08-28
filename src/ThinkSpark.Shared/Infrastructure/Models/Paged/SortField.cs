using System.Linq.Expressions;

namespace ThinkSpark.Shared.Infrastructure.Models.Paged
{
    public class SortField
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public SortField()
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public SortField(string name, bool? isDescending)
        {
            Name = name;
            IsDescending = isDescending;
        }

        /// <summary>
        /// Nome do campo que será ordenado
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Indica de a ordenação deve ser descentende, caso contrario será ascendente
        /// </summary>
        public bool? IsDescending { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Is<TEntity>(Expression<Func<TEntity, object>> expression) where TEntity : class
        {
            if (expression == null)
                return false;

            var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression)?.Operand as MemberExpression)?.Member;

            if (member == null)
                return false;

            if (Name == null)
                return false;

            var result = member.Name.ToLower().Equals(Name.ToLower());
            return result;
        }
    }
}
