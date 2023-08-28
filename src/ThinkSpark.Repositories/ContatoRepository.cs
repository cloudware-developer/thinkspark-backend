using Microsoft.Extensions.Configuration;
using ThinkSpark.Repositories.Entities;
using ThinkSpark.Shared.Infrastructure.Generics.Repository;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Base;

namespace ThinkSpark.Repositories
{
    public interface IContatoRepository : IRepositoryBase<Contato>
    {
    }

    public class ContatoRepository : RepositoryBase<Contato>, IContatoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDbContext _dbContext;

        public ContatoRepository(IConfiguration configuration, IDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
    }
}
