using ThinkSpark.Shared.Security.Aes.Models;

namespace ThinkSpark.Shared.Security.Aes.Interface
{
    public interface ICryptographyAes
    {
        /// <summary>
        /// Gera hash e key ou hash baseado na key, utilizando criptogradia Sha512 e retorno em Base64.
        /// Fornecendo key e value vazios, gera o hash e tambem gera a key. Fornecendo key e value, gera o hash
        /// baseado na key.
        /// </summary>
        /// <param name="key">Chave utilizada para criptografar o conteúdo.</param>
        /// <param name="value">Conteúdo a ser criptografado.</param>
        /// <returns>Retorna objeto <see cref="EncryptVm">EncryptData</see> com as informações do parâmetros informados.</returns>
        Encrypt? GenerateHash(string key, string value);

        /// <summary>
        /// Criptografa uma string utilizando tecnologia Advanced Encryption Standard (AES)
        /// </summary>
        /// <param name="value">String a ser criptografada no padrão AES.</param>
        /// <returns>Retorna a string criptografada.</returns>
        Task<string> EncryptAsync(string value);

        /// <summary>
        /// Descriptografa uma string utilizando tecnologia Advanced Encryption Standard (AES)
        /// </summary>
        /// <param name="value">String a ser descriptografada no padrão AES.</param>
        /// <returns>Retorna a string descriptografada.</returns>
        Task<string> DecryptAsync(string value);
    }
}
