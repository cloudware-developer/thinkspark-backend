namespace ThinkSpark.Shared.Cryptography.Md5.Interface
{
    /// <summary>
    /// MD5 é um algoritmo de hash criptográfico, não um algoritmo de criptografia. O MD5 gera um valor de hash que é único para cada entrada, mas não é reversível para obter a entrada original a partir do hash. Ele é frequentemente usado para verificar a integridade de dados, mas não deve ser usado para fins de criptografia ou segurança, pois é considerado fraco e vulnerável a ataques de colisão. Para criptografia segura, é recomendado o uso de algoritmos mais fortes, como AES.
    /// </summary>
    public interface ICryptographyMd5
    {
        string ObtemHashMd5(string value);
    }
}
