using Microsoft.AspNetCore.Mvc;
using ThinkSpark.Shared.Infrastructure.Models.Enums;

namespace ThinkSpark.Shared.Infrastructure.Generics.Controller.Interfaces
{
    public interface IGenericController<TEntity>
    {
        /// <summary>
        /// Adiciona um objeto. 
        /// </summary>
        /// <param name="entity">Objeto a ser adicionado.</param>
        [HttpPost("Adicionar")]
        Task<IActionResult> AdicionarAsync([FromBody] TEntity entity);

        /// <summary>
        /// Atualiza um objeto. 
        /// </summary>
        /// <param name="entity">Objeto a ser atualizado.</param>
        [HttpPut("Atualizar")]
        Task<IActionResult> AtualizarAsync([FromBody] TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        Task<IActionResult> ObtemItemPorFiltroAsync([FromQuery] int id);

        /// <summary>
        /// Deleta um objeto. 
        /// </summary>
        /// <param name="entity">Id do objeto a ser deletado.</param>
        [HttpDelete]
        Task<IActionResult> DeletarAsync([FromQuery] int id);

        /// <summary>
        /// Obtem uma lista de objetos para preencher uma combobox.
        /// </summary>
        [HttpGet("Combo")]
        Task<IActionResult> ObterComboAsync();

        /// <summary>
        /// Obtem uma lista de objetos paginada. 
        /// </summary>
        /// <param name="entity">Objeto dados da paginação.</param>
        [HttpGet("Paginacao")]
        Task<IActionResult> ObterPaginacaoAsync([FromQuery] TEntity? filters, int? currentPage = 1, int? itemsPerPage = 10, string? sortColumn = null, OrderByDirectionEnum sortDirection = OrderByDirectionEnum.Asc);
    }
}
