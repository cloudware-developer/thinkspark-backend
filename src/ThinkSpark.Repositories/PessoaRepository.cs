using Microsoft.Extensions.Configuration;
using ThinkSpark.Repositories.Entities;
using ThinkSpark.Shared.Infrastructure.Generics.Repository;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Base;

namespace ThinkSpark.Repositories
{
    public interface IPessoaRepository : IRepositoryBase<Pessoa>
    {
    }

    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDbContext _dbContext;

        public PessoaRepository(IConfiguration configuration, IDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
    }
}
