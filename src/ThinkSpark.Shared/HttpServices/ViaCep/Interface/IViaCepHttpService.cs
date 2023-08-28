using ThinkSpark.Shared.HttpServices.ViaCep.Models;

namespace ThinkSpark.Shared.HttpServices.ViaCep.Interface
{
    public interface IViaCepHttpService
    {
        Task<Zipcode> GetZipcodeAsync(string zipcode);
        Task<HttpResponseMessage> GetZipcodeWithResponseMessageAsync(string zipcode);
    }
}
