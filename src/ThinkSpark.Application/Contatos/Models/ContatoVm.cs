namespace ThinkSpark.Application.Contatos.Models
{
    public class ContatoVm
    {
        public int ContatoId { get; set; }
        public int PessoaId { get; set; }
        public int TipoContatoId { get; set; }
        public string? Descricao { get; set; }
        public ContatoVm() { }
    }
}
