using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ThinkSpark.Shared.Extensions.WebApi
{
    public static class SwaggerExtension
    {
        private static string _swaggerTitle = "ThinkSpark Api";
        private static string _swaggerVersion = "v1";
        private static string _swaggerUrl = "/swagger/v1/swagger.json";
        private static string _swaggerDescription = "Micro serviço de gestão de contatos.";

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Token de autenticação JWT",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme,
                            }
                        },
                        new string[] {}
                    }
                });

                c.SwaggerDoc(_swaggerVersion, new OpenApiInfo
                {
                    Version = _swaggerVersion,
                    Title = _swaggerTitle,
                    Description = _swaggerDescription,
                });
            })
           .AddRouting(options =>
           {
               options.LowercaseUrls = true;
               options.LowercaseQueryStrings = true;
               options.SuppressCheckForUnhandledSecurityMetadata = true;
               options.AppendTrailingSlash = true;
           });
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            return app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swagger, request) =>
                {
                    swagger.Servers = new List<OpenApiServer>
                    {
                       new OpenApiServer {Url = $"{request.Scheme}://{request.Host.Value}"}
                    };
                });
            })
           .UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint(_swaggerUrl, _swaggerTitle);
           });
        }
    }
}
