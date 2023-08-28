using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;
using ThinkSpark.Shared.HttpServices.Ibge;
using ThinkSpark.Shared.HttpServices.Ibge.Interface;
using ThinkSpark.Shared.HttpServices.ViaCep;
using ThinkSpark.Shared.HttpServices.ViaCep.Interface;

namespace ThinkSpark.Shared.Extensions.WebApi
{
    /// <summary>
    /// Extensão do HealthCheck customizada para esse microserviço.
    /// </summary>
    public static class HealthCheckExtension
    {
        private static string _tagSqlServer = "Banco transacional SqlServer";
        private static string _tagRedis = "Banco de cache Redis";
        private static string _tagViaCep = "Endpoint ViaCep";
        private static string _tagIbge = "Endpoint Ibge";

        /// <summary>
        /// Adiciona as configurações e definições do HealthChecks.
        /// </summary>
        /// <param name="services">Container de serviços do .NET.</param>
        /// <returns>Retorna o container com os serviços.</returns>
        public static IServiceCollection AddHealthChecksConfig(this IServiceCollection services)
        {
            try
            {
                services.AddHealthChecks().AddCheck<SqlServerHealthCheck>(_tagSqlServer);
                services.AddHealthChecks().AddCheck<RedisHealthCheck>(_tagRedis);
                services.AddHealthChecks().AddCheck<ViaCepCheck>(_tagViaCep);
                services.AddHealthChecks().AddCheck<IbgeCheck>(_tagIbge);
                services.AddHealthChecksUI(options =>
                {
                    options.SetEvaluationTimeInSeconds(5);  // 5 segundos.
                    //options.SetEvaluationTimeInSeconds(600);  // 600 segundos equivale a 10 minutos.
                    options.MaximumHistoryEntriesPerEndpoint(20); // Número máximo de checagens por endpoint.
                    options.AddHealthCheckEndpoint("Pharmlab.Api", "/healthchecks");
                })
                .AddInMemoryStorage(); //Aqui adicionamos o banco em memória para checagem conforme o tempo em SetEvaluationTimeInSeconds 

                return services;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    /// <summary>
    /// Executa a checagem para o HealthCheck se o banco de dados Redis esta no ar.
    /// </summary>
    public class ViaCepCheck : IHealthCheck
    {
        private readonly IViaCepHttpService _viaCepHttpService;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="viaCepHttpService">Interface de gerenciamento de Ceps.</param>
        public ViaCepCheck(IViaCepHttpService viaCepHttpService)
        {
            _viaCepHttpService = viaCepHttpService;
        }

        /// <summary>
        /// Efetua a checagem do serviço ViaCep.
        /// </summary>
        /// <param name="context">Interface de contexto de banco de dados.</param>
        /// <param name="cancellationToken">Objeto de cancelamento da requisição.</param>
        /// <returns>Retorna um <see cref="HealthCheckResult">HealthCheckResult</see></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var zipcode = "18111290";
            var results = await _viaCepHttpService.GetZipcodeWithResponseMessageAsync(zipcode);

            try
            {
                if (results.IsSuccessStatusCode)
                    return HealthCheckResult.Healthy($"Endpoint ViaCep Ok.");
                else
                    return HealthCheckResult.Unhealthy($"Endpoint ViaCep fora do ar.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Endpoint ViaCep fora do ar. {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Executa a checagem para o HealthCheck se o banco de dados Redis esta no ar.
    /// </summary>
    public class IbgeCheck : IHealthCheck
    {
        private readonly IIbgeHttpService _ibgeHttpService;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="viaCepHttpService">Interface de gerenciamento de municípios.</param>
        public IbgeCheck(IIbgeHttpService ibgeHttpService)
        {
            _ibgeHttpService = ibgeHttpService;
        }

        /// <summary>
        /// Efetua a checagem do serviço Ibge.
        /// </summary>
        /// <param name="context">Interface de contexto de banco de dados.</param>
        /// <param name="cancellationToken">Objeto de cancelamento da requisição.</param>
        /// <returns>Retorna um <see cref="HealthCheckResult">HealthCheckResult</see></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var results = await _ibgeHttpService.ObtemMunicipiosComResponseMessageAsync();

            try
            {
                if (results.IsSuccessStatusCode)
                    return HealthCheckResult.Healthy($"Endpoint Ibge Ok.");
                else
                    return HealthCheckResult.Unhealthy($"Endpoint Ibge fora do ar.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Endpoint Ibge fora do ar. {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Executa a checagem para o HealthCheck se o banco de dados Redis esta no ar.
    /// </summary>
    public class RedisHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="configuration">Interface que contém as opções de configuração deste serviço.</param>
        public RedisHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Efetua a checagem do serviço de banco de dados em memory.
        /// </summary>
        /// <param name="context">Interface de contexto de banco de dados.</param>
        /// <param name="cancellationToken">Objeto de cancelamento da requisição.</param>
        /// <returns>Retorna um <see cref="HealthCheckResult">HealthCheckResult</see></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var connectionString = _configuration.GetConnectionString("Cache");

            if (string.IsNullOrEmpty(connectionString))
                return HealthCheckResult.Unhealthy($"Banco de dados de cache Redis esta sem conexão configurada.");

            return await Task.Run(() =>
            {
                using (var connection = ConnectionMultiplexer.Connect(connectionString))
                {
                    try
                    {
                        var db = connection.GetDatabase(0);
                        return HealthCheckResult.Healthy($"Banco de dados de cache Redis Ok.");
                    }
                    catch (Exception ex)
                    {
                        return HealthCheckResult.Unhealthy($"Banco de dados de cache Redis fora do ar. {ex.Message}");
                    }
                }
            });
        }
    }

    /// <summary>
    /// Executa a checagem para o HealthCheck se o banco de dados SqlServer esta no ar.
    /// </summary>
    public class SqlServerHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="configuration">Interface que contém as opções de configuração deste serviço.</param>
        public SqlServerHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///  Efetua a checagem do serviço de banco de dados Microsoft SqlServer.
        /// </summary>
        /// <param name="context">Interface de contexto de banco de dados.</param>
        /// <param name="cancellationToken">Objeto de cancelamento da requisição.</param>
        /// <returns>Retorna um <see cref="HealthCheckResult">HealthCheckResult</see></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
                return HealthCheckResult.Unhealthy($"Banco de dados de Microsoft SqlServer esta sem conexão configurada.");

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    return HealthCheckResult.Healthy($"Banco de dados transacional SqlServer Ok.");
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Banco de dados transacional SqlServer fora do ar. {ex.Message}");
                }
            }
        }
    }
}
