using System.Security.Cryptography;
using System.Text;
using ThinkSpark.Shared.Cryptography.Md5.Interface;

namespace ThinkSpark.Shared.Cryptography.Md5
{
    /// <summary>
    /// MD5 é um algoritmo de hash criptográfico, não um algoritmo de criptografia. O MD5 gera um valor de hash que é único para cada entrada, mas não é reversível para obter a entrada original a partir do hash. Ele é frequentemente usado para verificar a integridade de dados, mas não deve ser usado para fins de criptografia ou segurança, pois é considerado fraco e vulnerável a ataques de colisão. Para criptografia segura, é recomendado o uso de algoritmos mais fortes, como AES.
    /// </summary>
    public class CryptographyMd5 : ICryptographyMd5
    {
        /// <summary>
        /// Recebe uma string como entrada e retorna a representação hexadecimal do valor 
        /// de hash MD5 calculado para essa string.
        /// </summary>
        /// <param name="value">Conteudo a ser criptografado no padrão Md5.</param>
        /// <returns>Retorna a representação hexadecimal do valor de hash MD5 calculado para essa string</returns>
        public string ObtemHashMd5(string value)
        {
            string result = string.Empty;

            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(value);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2")); // "x2" formata cada byte como um valor hexadecimal de dois dígitos
                }

                result = stringBuilder.ToString();
            }

            return result;
        }
    }
}
