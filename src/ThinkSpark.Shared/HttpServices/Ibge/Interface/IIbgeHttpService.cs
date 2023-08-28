using ThinkSpark.Shared.HttpServices.Ibge.Enums;
using ThinkSpark.Shared.HttpServices.Ibge.Models;

namespace ThinkSpark.Shared.HttpServices.Ibge.Interface
{
    /// <summary>
    /// Interface de comunicação com o serviço do Ibge. Link da documentação: https://servicodados.ibge.gov.br/api/docs
    /// </summary>
    public interface IIbgeHttpService
    {
        /// <summary>
        /// Obtem municípios brasileiros por uf.
        /// </summary>
        /// <param name="uf">uf</param>
        Task<List<City>> ObterMunicipiosPorUf(UfEnum uf);

        /// <summary>
        /// Obtem todos os municípios brasileiros ordenados por nome.
        /// </summary>
        Task<List<City>> ObtemMunicipios();

        /// <summary>
        /// Obtem todos os municípios brasileiros ordenados por nome, mas retorna o objeto HttpResponseMessage.
        /// </summary>
        Task<HttpResponseMessage> ObtemMunicipiosComResponseMessageAsync();
    }
}
