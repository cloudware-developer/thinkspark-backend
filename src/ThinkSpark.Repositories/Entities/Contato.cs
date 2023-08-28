namespace ThinkSpark.Repositories.Entities
{
    public class Contato
    {
        public int ContatoId { get; set; }
        public int PessoaId { get; set; }
        public int TipoContatoId { get; set; }
        public string? Descricao { get; set; }
        public Pessoa? Pessoa { get; set; }
        public Contato() { }
    }
}
