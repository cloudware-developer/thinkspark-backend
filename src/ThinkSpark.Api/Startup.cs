using ThinkSpark.Application.Contatos;
using ThinkSpark.Application.Pessoas;
using ThinkSpark.Repositories;
using ThinkSpark.Repositories.Context;
using ThinkSpark.Shared.Cryptography.Md5;
using ThinkSpark.Shared.Cryptography.Md5.Interface;
using ThinkSpark.Shared.Cryptography.Sha;
using ThinkSpark.Shared.Extensions.WebApi;
using ThinkSpark.Shared.HttpServices.Ibge;
using ThinkSpark.Shared.HttpServices.Ibge.Interface;
using ThinkSpark.Shared.HttpServices.Mail;
using ThinkSpark.Shared.HttpServices.Mail.Interface;
using ThinkSpark.Shared.HttpServices.ViaCep;
using ThinkSpark.Shared.HttpServices.ViaCep.Interface;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Base;
using ThinkSpark.Shared.Securiry.Aes;
using ThinkSpark.Shared.Security.Aes.Interface;
using ThinkSpark.Shared.Security.Token;
using ThinkSpark.Shared.Security.Token.Interface;

namespace ThinkSpark.Api
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddControllers();

                services.AddCorsPolicy();

                services.AddSwaggerConfig();

                services.AddJwtConfig(_configuration);

                services.AddAuthenticationCore();

                services.AddCustomDbContext<IDbContext, ContatosContext>(_configuration);

                services.AddScoped<IPessoaApplication, PessoaApplication>();
                services.AddScoped<IContatoApplication, ContatoApplication>();

                services.AddScoped<IPessoaRepository, PessoaRepository>();
                services.AddScoped<IContatoRepository, ContatoRepository>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao iniciar aplicação. {ex.Message}");
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseHttpLogging();

                app.UseCorsPolicy();

                app.UseSwagger();

                app.UseRouting();

                //app.UseAuthentication();

                //app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers().RequireAuthorization();
                });
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao iniciar aplicação. {ex.Message}");
            }
        }
    }
}
