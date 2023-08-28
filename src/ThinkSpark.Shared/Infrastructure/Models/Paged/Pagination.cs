namespace ThinkSpark.Shared.Infrastructure.Models.Paged
{
    public class Pagination
    {
        /// <summary>
        /// Índice do primeiro item da página
        /// </summary>
        public int ItemStartIndex { get; set; }

        /// <summary>
        /// Tamanho da página
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public Pagination() { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="itemStartIndex">Índice do primeiro item da página</param>
        /// <param name="pageSize">Tamanho da página</param>
        public Pagination(int itemStartIndex, int pageSize)
        {
            ItemStartIndex = itemStartIndex;
            PageSize = pageSize;
        }

        /// <summary>
        /// Verifica se os dados de paginação são válidos
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            var result = ItemStartIndex >= 0 && PageSize > 0;
            return result;
        }
    }

}
