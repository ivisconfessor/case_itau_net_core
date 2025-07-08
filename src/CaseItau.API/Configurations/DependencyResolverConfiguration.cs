using Asp.Versioning;
using CaseItau.Application.Interfaces.UseCases;
using CaseItau.Application.Mappings;
using CaseItau.Application.UseCases;
using CaseItau.Application.Validators.Funds;
using CaseItau.Domain.Interfaces.Repositories;
using CaseItau.Domain.Interfaces.Services;
using CaseItau.Infrastructure.Repositories;
using CaseItau.Infrastructure.Services;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CaseItau.API.Configurations
{
    public static class DependencyResolverConfiguration
    {
        public static void IntegraDependencyResolver(this IServiceCollection services)
        {
            RegisterUseCases(services);
            RegisterValidators(services);
            RegisterServices(services);
            RegisterRepositories(services);
            ConfigureMapster(services);
            ConfigureApiVersion(services);
            ConfigureSwagger(services);
            ConfigureHealthCheck(services);
            ConfigureCors(services);
        }

        private static void RegisterUseCases(IServiceCollection services)
        {
            services.AddScoped<ICreateFundUseCase, CreateFundUseCase>();
            services.AddScoped<IDeleteFundUseCase, DeleteFundUseCase>();
            services.AddScoped<IGetFundByCodeUseCase, GetFundByCodeUseCase>();
            services.AddScoped<IListAllFundsUseCase, ListAllFundsUseCase>();
            services.AddScoped<IUpdateFundNetWorthUseCase, UpdateFundNetWorthUseCase>();
            services.AddScoped<IUpdateFundUseCase, UpdateFundUseCase>();
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateFundRequestDtoValidator>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFundService, FundService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IFundRepository, FundRepository>();
        }

        private static void ConfigureMapster(IServiceCollection services)
        {
            MappingConfig.ConfigureMapster();
            services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            services.AddScoped<IMapper, ServiceMapper>();
        }

        private static void ConfigureApiVersion(IServiceCollection services)
        {
            
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "CaseItau.API",
                    Version = "v1",
                    Description = "API de gerenciamento de Fundos de Investimento.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Itaú Asset",
                        Email = "desenv-asset@itau-unibanco.com.br",
                        Url = new Uri("https://www.itauassetmanagement.com.br/sobre-nos/quem-somos/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        private static void ConfigureHealthCheck(IServiceCollection services)
        {
            var connectionString = "Data Source=dbCaseItau.s3db";

            services.AddHealthChecks()
            .AddSqlite(
                connectionString,
                name: "Base de Dados",
                timeout: TimeSpan.FromSeconds(5),
                tags: ["db", "data", "sql", "sqlite"]);

            // Configura o Health Checks UI
            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(15);
                options.MaximumHistoryEntriesPerEndpoint(50);
                options.SetApiMaxActiveRequests(1);
                options.AddHealthCheckEndpoint("API", "/api/health");
            })
            .AddInMemoryStorage();

            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(30);
            });
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });
        }
    }
}
