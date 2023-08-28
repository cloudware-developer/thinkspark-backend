using ThinkSpark.Shared.Extensions.Common;
using ThinkSpark.Shared.HttpServices.Ibge.Enums;
using ThinkSpark.Shared.HttpServices.Ibge.Interface;
using ThinkSpark.Shared.HttpServices.Ibge.Models;
using ThinkSpark.Shared.Infrastructure.Exceptions;

namespace ThinkSpark.Shared.HttpServices.Ibge
{
    /// <summary>
    /// Classe de comunicação com o serviço do Ibge. 
    /// Link da documentação: <see cref="https://servicodados.ibge.gov.br/api/docs"/>aqui</see>
    /// </summary>
    public class IbgeHttpService : IIbgeHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Construtor. Link da documentação: https://servicodados.ibge.gov.br/api/docs
        /// </summary>
        /// <param name="httpClient">Objeto de requisições http's.</param>
        public IbgeHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("IbgeHttpClient");
        }

        /// <summary>
        /// Obtem municípios brasileiros por uf.
        /// </summary>
        /// <param name="uf">Uf (estado brasileiro) ao qual se quer retornar os municípios.</param>
        public async Task<List<City>> ObterMunicipiosPorUf(UfEnum uf)
        {
            try
            {
                var content = new List<City>();
                var value = Enum.GetName(typeof(UfEnum), uf);

                using (var response = await _httpClient.GetAsync($"/api/v1/localidades/estados/{value}/municipios?orderBy=nome"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException($"Falha ao obter municípios.");
                    else
                        content = await response.Content.ReadAsAsync<List<City>>();
                }

                return content;
            }
            catch (RepositoryException ex)
            {
                throw ex.ExceptionHandler(nameof(ObterMunicipiosPorUf));
            }
        }

        /// <summary>
        /// Obtem todos os municípios brasileiros.
        /// </summary>
        public async Task<List<City>> ObtemMunicipios()
        {
            try
            {
                var content = new List<City>();

                using (var response = await _httpClient.GetAsync($"/api/v1/localidades/municipios?orderBy=nome"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException($"Falha ao obter municípios.");
                    else
                        content = await response.Content.ReadAsAsync<List<City>>();
                }

                return content;
            }
            catch (RepositoryException ex)
            {
                throw ex.ExceptionHandler(nameof(ObtemMunicipios));
            }
        }

        /// <summary>
        /// Obtem todos os municípios brasileiros, mas retorna o objeto HttpResponseMessage.
        /// </summary>
        public async Task<HttpResponseMessage> ObtemMunicipiosComResponseMessageAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(($"/api/v1/localidades/municipios?orderBy=nome"));
                return response;
            }
            catch (RepositoryException ex)
            {
                throw ex.ExceptionHandler(nameof(ObtemMunicipiosComResponseMessageAsync));
            }
        }
    }
}
