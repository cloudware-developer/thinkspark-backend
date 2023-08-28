using ThinkSpark.Application.Contatos.Models;

namespace ThinkSpark.Application.Pessoas.Models
{
    public class PessoaVm
    {
        public int PessoaId { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public bool? EmailConfirmado { get; set; }
        public string? Celular { get; set; }
        public bool? CelularConfirmado { get; set; }
        public DateTime Nascimento { get; set; }
        public List<ContatoVm>? Contato { get; set; }
        public string? Cpf { get; set; }
        public string? Rg { get; set; }
        public string? Senha { get; set; }
        public string? Foto { get; set; }
        public int? Status { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? EditadoEm { get; set; }
    }
}
