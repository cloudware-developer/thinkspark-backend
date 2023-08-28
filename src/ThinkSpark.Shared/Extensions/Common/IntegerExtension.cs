namespace ThinkSpark.Shared.Extensions.Common
{
    public static class IntegerExtension
    {
        /// <summary>
        /// Verifica se o inteiro é maior que o valor informado em comparator.
        /// </summary>
        /// <param name="i">inteiro</param>
        /// <param name="comparator">comparador</param>
        public static bool IsGreaterThan(this int i, int comparator)
        {
            var result = i > comparator;
            return result;
        }

        /// <summary>
        /// Verifica se um número é pas.
        /// </summary>
        /// <param name="number">Número a ser verificado.</param>
        public static bool IsEven(this int number)
        {
            var result = number % 2 == 0;
            return result;
        }

        /// <summary>
        /// Verifica se um número é impar.
        /// </summary>
        /// <param name="number">Número a ser verificado.</param>
        public static bool IsOdd(this int number)
        {
            var result = number % 2 != 0;
            return result;
        }
    }

}
