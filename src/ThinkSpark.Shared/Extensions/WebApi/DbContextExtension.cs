using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThinkSpark.Shared.Infrastructure.Generics.Repository.Interfaces.Base;
using static Humanizer.In;

namespace ThinkSpark.Shared.Extensions.WebApi
{
    public static class DbContextExtension
    {
        /// <summary>
        /// Registra um contexto de acesso a dados da aplicação.
        /// </summary>
        /// <param name="services">Container de serviços do .NET.</param>
        /// <param name="configuration">Configurações da aplicação.</param>
        /// <returns>Retorna o container com os serviços ja registrados.</returns>
        public static IServiceCollection AddCustomDbContext<TIDbContext, TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : DbContext
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped(typeof(TIDbContext), typeof(TDbContext));

            services.AddDbContext<TDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);                              
            });

            return services;
        }
    }
}
