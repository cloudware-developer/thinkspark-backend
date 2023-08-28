using Microsoft.AspNetCore.Mvc;
using ThinkSpark.Application.Pessoas;
using ThinkSpark.Application.Pessoas.Models;
using ThinkSpark.Shared.Infrastructure.Generics.Controller.Interfaces;
using ThinkSpark.Shared.Infrastructure.Models.Enums;

namespace ThinkSpark.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase, IGenericController<PessoaVm>
    {
        private readonly IConfiguration _configuration;
        private readonly IPessoaApplication _pessoaApplication;

        public PessoaController(
            IConfiguration configuration,
            IPessoaApplication pessoaApplication
        )
        {
            _configuration = configuration;
            _pessoaApplication = pessoaApplication;
        }

        /// <summary>
        /// Adiciona um objeto. 
        /// </summary>
        /// <param name="entity">Objeto a ser adicionado.</param>
        [HttpPost]
        public async Task<IActionResult> AdicionarAsync([FromBody] PessoaVm entity)
        {
            try
            {
                await _pessoaApplication.AdicionarAsync(entity);
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
        public async Task<IActionResult> AtualizarAsync([FromBody] PessoaVm entity)
        {
            try
            {
                await _pessoaApplication.AtualizarAsync(entity);
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
        public async Task<IActionResult> DeletarAsync([FromQuery] int id)
        {
            try
            {
                await _pessoaApplication.DeletarAsync(x => x.PessoaId == id);
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
                var item = await _pessoaApplication.ObterItemPorFiltroAsync(x => x.PessoaId == id);
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
                var collection = await _pessoaApplication.ObterComboAsync();
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
        public async Task<IActionResult> ObterPaginacaoAsync([FromQuery] PessoaVm? filters = null, int? currentPage = 1, int? itemsPerPage = 10, string? orderByColumn = null, OrderByDirectionEnum orderByDirection = OrderByDirectionEnum.Asc)
        {
            try
            {
                var collection = await _pessoaApplication.ObterPaginacaoAsync(filters, currentPage, itemsPerPage, orderByColumn, orderByDirection);
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}