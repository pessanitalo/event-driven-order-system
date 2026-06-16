using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using order_service.Domain.Repositories;
using order_service.Infrastructure.MessageBroker;
using order_service.Infrastructure.Persistence.Repository;


namespace order_service.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IOutboxMessagesRepository, OutboxMessagesRepository>();
            services.AddHostedService<OutboxPublisherWorker>();
            services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
            return services;
        }

        public static IServiceCollection AddObservability(this IServiceCollection services)
        {
              services.AddOpenTelemetry()
                    .ConfigureResource(resource => resource
                    .AddService(serviceName: "order-service"))
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddProcessInstrumentation()
                        .AddNpgsqlInstrumentation()
                        .AddPrometheusExporter();
                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddSource("Npgsql")
                              .AddOtlpExporter(otlp =>
                              {
                                  otlp.Endpoint = new Uri("http://localhost:4318/v1/traces");
                                  otlp.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                              }); ;
                });
            return services;
        }
    }
}
