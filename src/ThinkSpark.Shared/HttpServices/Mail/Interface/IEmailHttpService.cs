namespace ThinkSpark.Shared.HttpServices.Mail.Interface
{
    public interface IEmailHttpService
    {
        /// <summary>
        /// Envia e-mail de nova revalidacao de senha com o link gerado.
        /// </summary>
        /// <param name="nome">Nome da pessoa</param>
        /// <param name="email">E-mail da pessoa</param>
        /// <param name="link">Link para acesso a alteracação da senha.</param>
        void EnviaEmailDeRevalidacaoDeSenha(string nome, string email, string link);

        /// <summary>
        /// Envia e-mail de senha alterada com sucesso.
        /// </summary>
        /// <param name="nome">Nome da pessoa</param>
        /// <param name="email">E-mail da pessoa</param>
        void EnviaEmailDeRevalidacaoDeSenhaComSucesso(string nome, string email);
    }
}
