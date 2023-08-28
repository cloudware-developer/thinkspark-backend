using System.Net.Http;
using ThinkSpark.Shared.HttpServices.ViaCep.Interface;
using ThinkSpark.Shared.HttpServices.ViaCep.Models;

namespace ThinkSpark.Shared.HttpServices.ViaCep
{
    public class ViaCepHttpService : IViaCepHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Construtor. Link da documentação: https://viacep.com.br/
        /// </summary>
        /// <param name="httpClient">Objeto de requisições http's.</param>
        public ViaCepHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ViaCepHttpClient");
        }

        /// <summary>
        /// Obtem dados de localização de um determinado cep.
        /// </summary>
        /// <param name="zipcode">Cep a ser buscado.</param>
        public async Task<Zipcode> GetZipcodeAsync(string zipcode)
        {
            try
            {
                Zipcode content;

                using (var response = await _httpClient.GetAsync($"/ws/{zipcode}/json"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException($"Falha ao obter cep {zipcode}.");
                    else
                        content = await response.Content.ReadAsAsync<Zipcode>();
                }

                return content;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtem dados de localização de um determinado cep.
        /// </summary>
        /// <param name="zipcode">Cep a ser buscado.</param>
        public async Task<HttpResponseMessage> GetZipcodeWithResponseMessageAsync(string zipcode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/ws/{zipcode}/json");
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
