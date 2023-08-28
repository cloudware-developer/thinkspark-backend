using Microsoft.Extensions.Configuration;
using ThinkSpark.Application.Enums;
using ThinkSpark.Application.Pessoas.Models;
using ThinkSpark.Application.Pessoas.Validations;
using ThinkSpark.Repositories;
using ThinkSpark.Repositories.Entities;
using ThinkSpark.Shared.Cryptography.Sha.Interface;
using ThinkSpark.Shared.Extensions.Common;
using ThinkSpark.Shared.Infrastructure.Exceptions;
using ThinkSpark.Shared.Infrastructure.Generics.Application;
using ThinkSpark.Shared.Infrastructure.Generics.Application.Interfaces.Base;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Base;
using ThinkSpark.Shared.Infrastructure.Models.Enums;
using ThinkSpark.Shared.Infrastructure.Models.Paged;

namespace ThinkSpark.Application.Pessoas
{
    public interface IPessoaApplication : IApplicationBase<Pessoa, PessoaVm>
    {
        Task<Paged<List<PessoaVm>>> ObterPaginacaoAsync(PessoaVm? filter = null, int? currentPage = 1, int? itemsPerPage = 10, string? orderByColumn = null, OrderByDirectionEnum? orderByDirection = OrderByDirectionEnum.Asc);
    }

    public class PessoaApplication : ApplicationBase<Pessoa, PessoaVm>, IPessoaApplication
    {
        private readonly IDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IPessoaRepository _repository;

        public PessoaApplication(
            IDbContext dbContext,
            IConfiguration config,
            IPessoaRepository repository
        ) : base(dbContext)
        {
            _dbContext = dbContext;
            _config = config;
            _repository = repository;
        }

        public override async Task AdicionarAsync(PessoaVm model)
        {
            var validation = new PessoaValidation(AcaoEnum.Add).Validate(model);

            if (validation.IsValid)
            {
                await base.AdicionarAsync(model);
                return;
            }

            throw new EntityException(validation.Errors.FirstOrDefault().ErrorMessage);
        }

        public override async Task AtualizarAsync(PessoaVm model)
        {
            var validation = new PessoaValidation(AcaoEnum.Update).Validate(model);

            if (validation.IsValid)
            {
                await base.AtualizarAsync(model);
                return;
            }

            throw new EntityException(validation.Errors.FirstOrDefault().ErrorMessage);
        }

        public async Task<Paged<List<PessoaVm>>> ObterPaginacaoAsync(PessoaVm? filter = null, int? currentPage = 1, int? itemsPerPage = 10, string? orderByColumn = null, OrderByDirectionEnum? orderByDirection = OrderByDirectionEnum.Asc)
        {
            try
            {
                var collection = _repository.ObterListaPorFiltroQueryable();

                if (filter != null)
                {
                    filter.ToUpperFields();

                    if (filter.PessoaId > 0)
                        collection = collection.Where(x => x.PessoaId == filter.PessoaId);

                    if (!string.IsNullOrEmpty(filter.Nome))
                        collection = collection.Where(x => x.Nome.Contains(filter.Nome));

                    if (!string.IsNullOrEmpty(filter.Email))
                        collection = collection.Where(x => x.Email.Contains(filter.Email));

                    if (!string.IsNullOrEmpty(filter.Celular))
                        collection = collection.Where(x => x.Celular.Contains(filter.Celular));

                    if (filter.CelularConfirmado != null)
                        collection = collection.Where(x => x.CelularConfirmado == filter.CelularConfirmado);

                    if (filter.EmailConfirmado != null)
                        collection = collection.Where(x => x.EmailConfirmado == filter.EmailConfirmado);

                    if (filter.Status > 0)
                        collection = collection.Where(x => x.Status == filter.Status);
                }

                Paged<List<PessoaVm>> result = CalculaPaginacao(currentPage, itemsPerPage, collection, x => x.PessoaId, orderByDirection);

                if (orderByColumn != null)
                {
                    if (orderByColumn.Equals("pessoaId"))
                        result = CalculaPaginacao(currentPage, itemsPerPage, collection, x => x.PessoaId, orderByDirection);

                    if (orderByColumn.Equals("nome"))
                        result = CalculaPaginacao(currentPage, itemsPerPage, collection, x => x.Nome, orderByDirection);

                    if (orderByColumn.Equals("email"))
                        result = CalculaPaginacao(currentPage, itemsPerPage, collection, x => x.Email, orderByDirection);

                    if (orderByColumn.Equals("celular"))
                        result = CalculaPaginacao(currentPage, itemsPerPage, collection, x => x.Celular, orderByDirection);

                    if (orderByColumn.Equals("status"))
                        result = CalculaPaginacao(currentPage, itemsPerPage, collection, x => x.Status, orderByDirection);

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
