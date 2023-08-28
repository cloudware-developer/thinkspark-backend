using Microsoft.Extensions.Configuration;
using ThinkSpark.Application.Contatos.Models;
using ThinkSpark.Application.Contatos.Validations;
using ThinkSpark.Application.Enums;
using ThinkSpark.Repositories;
using ThinkSpark.Repositories.Entities;
using ThinkSpark.Shared.Extensions.Common;
using ThinkSpark.Shared.Infrastructure.Exceptions;
using ThinkSpark.Shared.Infrastructure.Generics.Application;
using ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Base;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Base;
using ThinkSpark.Shared.Infrastructure.Models.Enums;
using ThinkSpark.Shared.Infrastructure.Models.Paged;

namespace ThinkSpark.Application.Contatos
{
    public interface IContatoApplication : IApplicationBase<Contato, ContatoVm>
    {
        Task<Paged<List<ContatoVm>>> ObterPaginacaoAsync(ContatoVm? filter = null, int? currentPage = 1, int? itemsPerPage = 10, string? orderByColumn = null, OrderByDirectionEnum? orderByDirection = OrderByDirectionEnum.Asc);
    }

    public class ContatoApplication : ApplicationBase<Contato, ContatoVm>, IContatoApplication
    {
        private readonly IDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IContatoRepository _repository;

        public ContatoApplication(
            IDbContext dbContext,
            IConfiguration config,
            IContatoRepository repository
        ) : base(dbContext)
        {
            _dbContext = dbContext;
            _config = config;
            _repository = repository;
        }

        public override async Task AdicionarAsync(ContatoVm model)
        {
            var validation = new ContatoValidation(AcaoEnum.Add).Validate(model);

            if (validation.IsValid)
            {
                await base.AdicionarAsync(model);
                return;
            }

            throw new EntityException(validation.Errors.FirstOrDefault().ErrorMessage);
        }

        public async Task<Paged<List<ContatoVm>>> ObterPaginacaoAsync(ContatoVm? filter = null, int? currentPage = 1, int? itemsPerPage = 10, string? orderByColumn = null, OrderByDirectionEnum? orderByDirection = OrderByDirectionEnum.Asc)
        {
            try
            {
                var collection = _repository.ObterListaPorFiltroQueryable();

                if (filter != null)
                {
                    filter.ToUpperFields();

                    if (filter.PessoaId > 0)
                        collection = collection.Where(x => x.PessoaId == filter.PessoaId);

                    if (filter.ContatoId > 0)
                        collection = collection.Where(x => x.ContatoId == filter.ContatoId);

                    if (filter.TipoContatoId > 0)
                        collection = collection.Where(x => x.TipoContatoId == filter.TipoContatoId);

                    if (!string.IsNullOrEmpty(filter.Descricao))
                        collection = collection.Where(x => x.Descricao!.Contains(filter.Descricao));
                }

                Paged<List<ContatoVm>> result = CalculaPaginacao(currentPage, itemsPerPage, collection, x => x.ContatoId, orderByDirection);

                if (orderByColumn != null)
                {
                    if (orderByColumn.Equals("contatoId"))
                        result = CalculaPaginacao(currentPage, itemsPerPage, collection, x => x.ContatoId, orderByDirection);

                    if (orderByColumn.Equals("descricao"))
                        result = CalculaPaginacao(currentPage, itemsPerPage, collection, x => x.Descricao, orderByDirection);

                    result.OrderByColumn = orderByColumn;
                }

                return await Task.FromResult(result);
            }
            catch (CommercialException ex)
            {
                throw ex.ExceptionHandler(nameof(ObterPaginacaoAsync));
            }
        }
    }
}
