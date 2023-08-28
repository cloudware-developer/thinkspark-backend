using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace ThinkSpark.Shared.Cryptography.Sha
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

    public class CryptographySha : ICryptographySha
    {
        private readonly IConfiguration _configuration;
        private readonly string _key;
        private readonly string _vector;

        public CryptographySha(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = _configuration.GetSection("CryptographyConfig:AdvancedEncryptionStandard:Key").Value;
            _vector = _configuration.GetSection("CryptographyConfig:AdvancedEncryptionStandard:Vector").Value;

            if (string.IsNullOrEmpty(_key))
                throw new ArgumentException("Chave de criptografia vazia no arquivo de configurações do serviço.");

            if (string.IsNullOrEmpty(_vector))
                throw new ArgumentException("Vetor de criptografia vezio no arquivo de configurações do serviço.");
        }

        /// <summary>
        /// Obtem uma string como entrada e retorna a representação hexadecimal do valor de hash SHA-1 calculado para essa string.
        /// </summary>
        /// <param name="value">Conteúdo a ser criptografado.</param>
        /// <returns>Retorna a representação hexadecimal do valor de hash SHA-1 calculado para essa string</returns>
        public string ObtemHashSha1(string value)
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(value);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" formata cada byte como um valor hexadecimal de dois dígitos
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Obtem uma string como entrada e retorna a representação hexadecimal do valor de hash SHA-256 calculado para essa string.
        /// </summary>
        /// <param name="value">Conteúdo a ser criptografado.</param>
        /// <returns>Retorna a representação hexadecimal do valor de hash SHA-256 calculado para essa string</returns>
        public string ObtemHashSha256(string value)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(value);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" formata cada byte como um valor hexadecimal de dois dígitos
                }

                return sb.ToString();
            }
        }

    }
}
