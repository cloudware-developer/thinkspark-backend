using FluentValidation;
using ThinkSpark.Application.Enums;
using ThinkSpark.Application.Pessoas.Models;

namespace ThinkSpark.Application.Pessoas.Validations
{
    public class PessoaValidation : AbstractValidator<PessoaVm>
    {
        public PessoaValidation(AcaoEnum actionType)
        {
            if (actionType == AcaoEnum.Add)
            {
                RuleFor(x => x.Nome).NotEmpty().WithMessage("Informe o nome.").Length(3, 200).WithMessage("O nome deve ter no mínimo 3 e no máximo 200 caracteres.");
                RuleFor(x => x.Email.ToLower()).NotEmpty().WithMessage("Informe o e-mail.").EmailAddress().WithMessage("Informe um email válido.");
                RuleFor(x => x.Celular).NotEmpty().WithMessage("Informe o telefone ou celular.").Length(11, 15).WithMessage("O telefone ou celular somente números");
                RuleFor(x => x.Cpf).NotEmpty().WithMessage("Informe o nome.").Length(11, 14).WithMessage("Informe o campo cpf ou cnpj do cliente.");                
                RuleFor(x => x.Senha).NotEmpty().WithMessage("Informe um senha.").Length(5, 15).WithMessage("A senha deve ter no mínimo 5 e no máximo 15 caracteres.");               
            }
            else if (actionType == AcaoEnum.Update)
            {
                RuleFor(x => x.PessoaId).GreaterThan(0).WithMessage("Informe o código efetuar a atualização.");
                RuleFor(x => x.Nome).NotEmpty().WithMessage("Informe o nome.").Length(3, 200).WithMessage("O nome deve ter no mínimo 3 e no máximo 200 caracteres.");
                RuleFor(x => x.Email.ToLower()).NotEmpty().WithMessage("Informe o email.").EmailAddress().WithMessage("Informe um email válido.");
                RuleFor(x => x.Celular).NotEmpty().WithMessage("Informe o telefone ou celular.").Length(11, 15).WithMessage("O telefone ou celular somente números");
                RuleFor(x => x.Cpf).NotEmpty().WithMessage("Informe o nome.").Length(11, 14).WithMessage("Informe o campo cpf ou cnpj do cliente.");
                RuleFor(x => x.Senha).NotEmpty().WithMessage("Informe um senha.").Length(5, 15).WithMessage("A senha deve ter no mínimo 5 e no máximo 15 caracteres.");
            }
        }
    }
}
