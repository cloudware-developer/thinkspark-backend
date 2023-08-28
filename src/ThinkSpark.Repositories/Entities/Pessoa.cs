namespace ThinkSpark.Repositories.Entities
{
    public class Pessoa
    {
        public int PessoaId { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailConfirmado { get; set; }
        public string Celular { get; set; } = null!;
        public bool CelularConfirmado { get; set; }
        public DateTime Nascimento { get; set; }
        public List<Contato>? Contato { get; set; }
        public string Cpf { get; set; } = null!;
        public string? Rg { get; set; }
        public string Senha { get; set; } = null!;
        public string? Foto { get; set; }
        public int Status { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? EditadoEm { get; set; }
    }
}
