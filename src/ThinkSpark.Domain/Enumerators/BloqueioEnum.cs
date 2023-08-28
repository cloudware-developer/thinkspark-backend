using System.ComponentModel;

namespace ThinkSpark.Domain.Enumerators
{
    public enum BloqueioEnum 
    {
        [Description("Inativo")]
        Inativo = 0,

        [Description("Ativo")]
        Ativo = 1,

        [Description("Bloqueado")]
        Bloqueado = 2,

        [Description("LembrarSenha")]
        LembrarSenha = 3
    }
}
