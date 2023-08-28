using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkSpark.Shared.HttpServices.ViaCep.Models
{
    /// <summary>
    /// Classe retornada pelos serviços da ViaCep.
    /// </summary>
    public class Zipcode
    {
        /// <summary>
        /// Cep
        /// </summary>
        public string Cep { get; set; } = null!;
        /// <summary>
        /// Logradouro ou conhecido como Endereço.
        /// </summary>
        public string Logradouro { get; set; } = null!;
        /// <summary>
        /// Complemento.
        /// </summary>
        public string Complemento { get; set; } = null!;
        /// <summary>
        /// Bairro
        /// </summary>
        public string Bairro { get; set; } = null!;
        /// <summary>
        /// Localidade
        /// </summary>
        public string Localidade { get; set; } = null!;
        /// <summary>
        /// Uf conhecido como Estado.
        /// </summary>
        public string Uf { get; set; } = null!;
        /// <summary>
        /// Código registrado no Ibge.
        /// </summary>
        public string Ibge { get; set; } = null!;
        /// <summary>
        /// ??
        /// </summary>
        public string Gia { get; set; } = null!;
        /// <summary>
        /// Número de DDD do município.
        /// </summary>
        public string Ddd { get; set; } = null!;
        /// <summary>
        /// ??
        /// </summary>
        public string Siafi { get; set; } = null!;
    }
}
