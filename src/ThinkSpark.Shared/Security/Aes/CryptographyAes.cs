using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using ThinkSpark.Shared.Security.Aes.Interface;
using ThinkSpark.Shared.Security.Aes.Models;

namespace ThinkSpark.Shared.Securiry.Aes
{
    public class CryptographyAes : ICryptographyAes
    {
        private string _encryptionKey;
        private string _encryptioVector;
        private readonly IConfiguration _configuration;

        public CryptographyAes(IConfiguration configuration)
        {
            _configuration = configuration;
            _encryptionKey = configuration.GetSection("CryptographyConfig:AdvancedEncryptionStandard:Key").Value;
            _encryptioVector = configuration.GetSection("CryptographyConfig:AdvancedEncryptionStandard:Vector").Value;

            if (string.IsNullOrEmpty(_encryptionKey))
                throw new ArgumentException("Chave de criptografia vazia no arquivo de configurações do serviço.");

            if (string.IsNullOrEmpty(_encryptioVector))
                throw new ArgumentException("Vetor da chave de criptografia vazio no arquivo de configurações do serviço.");
        }

        /// <summary>
        /// Gera hash e key ou hash baseado na key, utilizando criptogradia Sha512 e retorno em Base64.
        /// Fornecendo key e value vazios, gera o hash e tambem gera a key. Fornecendo key e value, gera o hash
        /// baseado na key.
        /// </summary>
        /// <param name="key">Chave utilizada para criptografar o conteúdo.</param>
        /// <param name="value">Conteúdo a ser criptografado.</param>
        /// <returns>Retorna objeto <see cref="EncryptVm">EncryptData</see> com as informações do parâmetros informados.</returns>
        public Encrypt? GenerateHash(string key, string value)
        {
            try
            {
                var password = value == null ? string.Empty : value;
                var encryptData = new Encrypt(key, password);

                if (!string.IsNullOrWhiteSpace(key))
                {
                    using (var hmac = new HMACSHA512(Convert.FromBase64String(key)))
                    {
                        encryptData.Hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(string.IsNullOrEmpty(encryptData.Password) ? string.Empty : encryptData.Password)));
                    }
                }
                else
                {
                    using (var hmac = new HMACSHA512())
                    {
                        encryptData.Key = Convert.ToBase64String(hmac.Key);
                        encryptData.Hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(string.IsNullOrEmpty(encryptData.Password) ? string.Empty : encryptData.Password)));
                    }
                }

                return encryptData;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Criptografa uma string utilizando tecnologia Advanced Encryption Standard (AES)
        /// </summary>
        /// <param name="value">String a ser criptografada no padrão AES.</param>
        /// <returns>Retorna a string criptografada.</returns>
        public async Task<string> EncryptAsync(string value)
        {
            return await Task.Run(() =>
            {
                try
                {
                    byte[] encrypted;

                    using (System.Security.Cryptography.Aes aesAlgorithm = System.Security.Cryptography.Aes.Create())
                    {
                        var encondedKey = new UTF8Encoding().GetBytes(_encryptionKey);
                        var base64String = Convert.ToBase64String(encondedKey);

                        aesAlgorithm.Key = Convert.FromBase64String(Convert.ToBase64String(encondedKey));
                        aesAlgorithm.IV = new UTF8Encoding().GetBytes(_encryptioVector);
                        aesAlgorithm.Padding = PaddingMode.Zeros;

                        ICryptoTransform cryptoTransform = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                            {
                                using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                                {
                                    streamWriter.Write(value);
                                }
                                encrypted = memoryStream.ToArray();
                            }
                        }
                    }

                    var encryptedValue = Convert.ToBase64String(encrypted);
                    return encryptedValue;
                }

                catch (Exception ex)
                {
                    var t = ex.Message;
                    return string.Empty;
                }
            });
        }

        /// <summary>
        /// Descriptografa uma string utilizando tecnologia Advanced Encryption Standard (AES)
        /// </summary>
        /// <param name="value">String a ser descriptografada no padrão AES.</param>
        /// <returns>Retorna a string descriptografada.</returns>
        public async Task<string> DecryptAsync(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return string.Empty;
                }

                string decrypted = string.Empty;

                using (System.Security.Cryptography.Aes aesAlgorithm = System.Security.Cryptography.Aes.Create())
                {
                    aesAlgorithm.Key = Convert.FromBase64String(Convert.ToBase64String(new UTF8Encoding().GetBytes(_encryptionKey)));
                    aesAlgorithm.IV = new UTF8Encoding().GetBytes(_encryptioVector);
                    aesAlgorithm.Padding = PaddingMode.Zeros;

                    ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor(aesAlgorithm.Key, aesAlgorithm.IV);

                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(value.Replace(" ", "+"))))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(cryptoStream))
                            {
                                decrypted = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }

                var result = await Task.FromResult(decrypted.Replace((char)22, (char)32).Replace((char)23, (char)32).Replace("\0", string.Empty));
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
