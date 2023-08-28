namespace ThinkSpark.Shared.Security.Aes.Models
{
    public class Encrypt
    {
        public string? Key { get; set; }
        public string? Password { get; set; }
        public string? Hash { get; set; }

        public Encrypt(string key, string password)
        {
            Key = key;
            Password = password;
        }

        public Encrypt(string key, string password, string hash)
        {
            Key = key;
            Password = password;
            Hash = hash;
        }
    }
}
