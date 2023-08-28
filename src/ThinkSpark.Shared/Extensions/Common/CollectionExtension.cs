namespace ThinkSpark.Shared.Extensions.Common
{
    public static class CollectionExtension
    {
        /// <summary>
        /// Método de extensão para distinguir itens por propriedade específica.
        /// </summary>
        /// <param name="keySelector">Função com a propriedade a ser verificado a distinção.</param>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
                if (seenKeys.Add(keySelector(element)))
                    yield return element;
        }
    }
}
