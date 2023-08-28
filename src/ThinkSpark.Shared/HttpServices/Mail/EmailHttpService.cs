using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using ThinkSpark.Shared.Extensions.Common;
using ThinkSpark.Shared.HttpServices.Mail.Interface;
using ApplicationException = ThinkSpark.Shared.Infrastructure.Exceptions.ApplicationException;

namespace ThinkSpark.Shared.HttpServices.Mail
{
    public class EmailHttpService : IEmailHttpService
    {
        private int _mailPort { get; set; }
        private string _mailHost { get; set; }
        private string _mailUser { get; set; }
        private string _mailPassword { get; set; }
        private bool _isBodyHtml { get; set; }
        private SmtpClient _smtpClient { get; set; }
        private IConfiguration _configuration { get; set; }
        public EmailHttpService(IConfiguration configuration)
        {
            _configuration = configuration;
            _mailPort = _configuration.GetSection("MailConfig").GetValue<int>("Port");
            _mailHost = _configuration.GetSection("MailConfig").GetValue<string>("Host");
            _mailUser = _configuration.GetSection("MailConfig").GetValue<string>("User");
            _mailPassword = _configuration.GetSection("MailConfig").GetValue<string>("Pass");
            _isBodyHtml = true;
            _smtpClient = new SmtpClient();
        }
        public void EnviaEmailDeRevalidacaoDeSenha(string nome, string email, string link)
        {
            var emailTo = new MailAddress(email);
            var emailFrom = new MailAddress(_mailUser);
            var message = new MailMessage(emailFrom, emailTo);

            message.Subject = "Sistema - Solicitação de Alteração de Senha";

            var content = new StringBuilder();
            content.AppendLine($"Olá {nome.ToCapitalize()}<br/>");
            content.AppendLine($"<br/>");
            content.AppendLine($"Você solicitou uma alteração de senha, para isso clique no link<br/>");
            content.AppendLine($"http://localhost:4200/novasenha/{link}<br/>");
            content.AppendLine($"<br/>");
            content.AppendLine($"A equipe de suporte agradece!<br/>");
            content.AppendLine($"Atenciosamente.<br/>");

            message.Body = content.ToString();
            message.IsBodyHtml = _isBodyHtml;

            _smtpClient.Host = _mailHost;
            _smtpClient.Port = _mailPort;
            _smtpClient.Credentials = new NetworkCredential(_mailUser, _mailPassword);
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.EnableSsl = true;

            try
            {
                _smtpClient.Send(message);
            }
            catch (SmtpException ex)
            {
                throw new Exception($"Erro ao enviar e-mail de revalidação de senha com link. Erro {ex.Message}");
            }
        }
        public void EnviaEmailDeRevalidacaoDeSenhaComSucesso(string nome, string email)
        {
            var emailTo = new MailAddress(email);
            var emailFrom = new MailAddress(_mailUser);
            var message = new MailMessage(emailFrom, emailTo);

            message.Subject = "Sistema - Senha alterada com Sucesso";

            var content = new StringBuilder();
            content.AppendLine($"Olá {nome.ToCapitalize()}<br/>");
            content.AppendLine($"<br/>");
            content.AppendLine($"Sua senha de acesso ao sistema alterada com sucesso.<br/>");
            content.AppendLine($"<br/>");
            content.AppendLine($"A equipe de suporte agradece!<br/>");
            content.AppendLine($"Atenciosamente.<br/>");

            message.Body = content.ToString();
            message.IsBodyHtml = _isBodyHtml;

            _smtpClient.Host = _mailHost;
            _smtpClient.Port = _mailPort;
            _smtpClient.Credentials = new NetworkCredential(_mailUser, _mailPassword);
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.EnableSsl = true;

            try
            {
                _smtpClient.Send(message);
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException($"Erro ao enviar e-mail de revalidação de senha efetuada com sucesso. Erro {ex.Message}");
            }
        }
    }
}
