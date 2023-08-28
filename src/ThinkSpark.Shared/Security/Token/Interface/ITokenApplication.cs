using System.Security.Claims;

namespace ThinkSpark.Shared.Security.Token.Interface
{
    public interface ITokenApplication
    {
        public string CriarToken(Claim[] claims);
        public string CriarTokenWithExpireTime(Claim[] claims, int expiresDays);
        public List<Claim> DescriptografarToken(string token);
    }
}
