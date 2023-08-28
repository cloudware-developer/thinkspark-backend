using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ThinkSpark.Repositories.Configuration;
using ThinkSpark.Repositories.Entities;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Base;

namespace ThinkSpark.Repositories.Context
{
    public class ContatosContext : DbContext, IDbContext
    {
        public DbSet<Pessoa>? Pessoa { get; set; }
        public DbSet<Contato>? Contato { get; set; }
        public override DatabaseFacade Database => base.Database;
        public ContatosContext(DbContextOptions<ContatosContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new PessoaConfiguration().Configure(modelBuilder.Entity<Pessoa>());
        }
    }
}
