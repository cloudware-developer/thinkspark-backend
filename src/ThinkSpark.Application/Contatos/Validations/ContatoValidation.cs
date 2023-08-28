using FluentValidation;
using ThinkSpark.Application.Contatos.Models;
using ThinkSpark.Application.Enums;

namespace ThinkSpark.Application.Contatos.Validations
{
    public class ContatoValidation : AbstractValidator<ContatoVm>
    {
        public ContatoValidation(AcaoEnum actionType)
        {
            if (actionType == AcaoEnum.Add)
            {
                RuleFor(x => x.PessoaId).GreaterThan(0).WithMessage("Informe o código da pessoa ao qual esse contato pertence.");
                RuleFor(x => x.TipoContatoId).GreaterThan(0).WithMessage("Informe o tipo de contato.");
                RuleFor(x => x.Descricao).NotEmpty().WithMessage("Informe o contato.").Length(3, 200).WithMessage("O contato deve ter no mínimo 3 e no máximo 200 caracteres.");
            }
        }
    }
}
