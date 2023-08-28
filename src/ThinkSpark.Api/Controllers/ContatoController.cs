using Microsoft.AspNetCore.Mvc;
using ThinkSpark.Application.Contatos;
using ThinkSpark.Application.Contatos.Models;
using ThinkSpark.Shared.Infrastructure.Generics.Controller.Interfaces;
using ThinkSpark.Shared.Infrastructure.Models.Enums;

namespace ThinkSpark.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase, IGenericController<ContatoVm>
    {
        private readonly IConfiguration _configuration;
        private readonly IContatoApplication _contatoApplication;

        public ContatoController(
            IConfiguration configuration,
            IContatoApplication contatoApplication
        )
        {
            _configuration = configuration;
            _contatoApplication = contatoApplication;
        }

        /// <summary>
        /// Adiciona um objeto. 
        /// </summary>
        /// <param name="entity">Objeto a ser adicionado.</param>
        [HttpPost]
        public async Task<IActionResult> AdicionarAsync([FromBody] ContatoVm entity)
        {
            try
            {
                await _contatoApplication.AdicionarAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um objeto. 
        /// </summary>
        /// <param name="entity">Objeto a ser atualizado.</param>
        [HttpPut]
        public async Task<IActionResult> AtualizarAsync([FromBody] ContatoVm entity)
        {
            try
            {
                await _contatoApplication.AtualizarAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Deleta um objeto. 
        /// </summary>
        /// <param name="entity">Id do objeto a ser deletado.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAsync([FromRoute] int id)
        {
            try
            {
                await _contatoApplication.DeletarAsync(x => x.PessoaId == id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Obtem um objeto pelo seu id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtemItemPorFiltroAsync([FromQuery] int id)
        {
            try
            {
                var item = await _contatoApplication.ObterItemPorFiltroAsync(x => x.ContatoId == id);
                return (item == null) ? NoContent() : Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Obtem uma lista de objetos para preencher uma combobox.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ObterComboAsync()
        {
            try
            {
                var collection = await _contatoApplication.ObterComboAsync();
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Obtem uma lista de objetos paginada podendo ser ordenada por uma determinada coluna. 
        /// </summary>
        /// <param name="entity">Objeto dados da paginação.</param>
        [HttpGet("Paginacao")]
        public async Task<IActionResult> ObterPaginacaoAsync([FromQuery] ContatoVm? filters = null, int? currentPage = 1, int? itemsPerPage = 10, string? orderByColumn = null, OrderByDirectionEnum orderByDirection = OrderByDirectionEnum.Asc)
        {
            try
            {
                var collection = await _contatoApplication.ObterPaginacaoAsync(filters, currentPage, itemsPerPage, orderByColumn, orderByDirection);
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}