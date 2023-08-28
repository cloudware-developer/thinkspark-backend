using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThinkSpark.Repositories.Entities;

namespace ThinkSpark.Repositories.Configuration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.Navigation(x => x.Contato).AutoInclude();
            builder.HasData(
                new Pessoa() { PessoaId = 1, Nome = "DALE K. MORRIS", Email = "morris@hotmail.com", EmailConfirmado = true, Celular = "7206846556", CelularConfirmado = true, Nascimento = new DateTime(1984, 01, 18), Cpf = "63089812061", Rg = "69139726010", Senha = "b3d312594eda53ca6896ed30b539b899cac892c843221faca1c6d7a46dce1623", Foto = "", Status = 1, CriadoEm = DateTime.Now, EditadoEm = null },
                new Pessoa() { PessoaId = 2, Nome = "PABLO G. COOPER", Email = "cooper@hotmail.com", EmailConfirmado = true, Celular = "4803280179", CelularConfirmado = false, Nascimento = new DateTime(1984, 01, 18), Cpf = "92783770075", Rg = "21878983008", Senha = "b3d312594eda53ca6896ed30b539b899cac892c843221faca1c6d7a46dce1623", Foto = "", Status = 1, CriadoEm = DateTime.Now, EditadoEm = null },
                new Pessoa() { PessoaId = 3, Nome = "CURTIS E. CHATTERTON", Email = "chatterton@hotmail.com", EmailConfirmado = true, Celular = "9254135709", CelularConfirmado = false, Nascimento = new DateTime(1984, 01, 18), Cpf = "22523862077", Rg = "58549417084", Senha = "b3d312594eda53ca6896ed30b539b899cac892c843221faca1c6d7a46dce1623", Foto = "", Status = 1, CriadoEm = DateTime.Now, EditadoEm = null }
            );
        }
    }
}
