using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ThinkSpark.Shared.Extensions.WebApi
{
    public static class CorsExtension
    {
        private const string _corsPolicyName = "AllowAll";

        /// <summary>
        /// Adiciona as configurações e definições da política de Cross-Origin Resource Sharing (CORS).
        /// </summary>
        /// <param name="services">Container de serviços do .NET.</param>
        /// <returns>Retorna o container com os serviços com a politica de Cross-Origin Resource Sharing (CORS) ja configurada.</returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(_corsPolicyName,
                    builder => builder
                        .WithOrigins(
                            "http://localhost:4200", "http://localhost:4444")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("x-custom-header")
                );
            });
        }

        /// <summary>
        /// Adiciona as configurações e definições da política de Cross-Origin Resource Sharing (CORS).
        /// </summary>
        /// <param name="services">Container de serviços do .NET.</param>
        /// <param name="origins">Lista de endereços de origem que podem ter acesso.</param>
        /// <returns>Retorna o container com os serviços com a politica de Cross-Origin Resource Sharing (CORS) ja configurada.</returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, List<string> origins)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(_corsPolicyName,
                    builder => builder
                        .WithOrigins(origins.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("x-custom-header")
                );
            });
        }

        /// <summary>
        /// Usa as definições de api para Cross-Origin Resource Sharing (CORS).
        /// </summary>
        /// <param name="app">Interface de construção da aplicação.</param>
        /// <returns>Retorna a interface de construção da aplicação.</returns>
        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            // return app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            return app.UseCors(_corsPolicyName);
        }
    }
}
