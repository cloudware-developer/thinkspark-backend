namespace ThinkSpark.Shared.Cryptography.Sha.Interface
{
    public interface ICryptographySha
    {
        /// <summary>
        /// Obtem uma string como entrada e retorna a representação hexadecimal do valor de hash SHA-1 calculado para essa string.
        /// </summary>
        /// <param name="value">Conteúdo a ser criptografado.</param>
        /// <returns>Retorna a representação hexadecimal do valor de hash SHA-1 calculado para essa string</returns>
        public string ObtemHashSha1(string value);

        /// <summary>
        /// Obtem uma string como entrada e retorna a representação hexadecimal do valor de hash SHA-256 calculado para essa string.
        /// </summary>
        /// <param name="value">Conteúdo a ser criptografado.</param>
        /// <returns>Retorna a representação hexadecimal do valor de hash SHA-256 calculado para essa string</returns>
        public string ObtemHashSha256(string value);
    }
}
