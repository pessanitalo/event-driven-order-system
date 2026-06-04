using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using order_payment.Application.Consumers;
using order_payment.domain.Ports;
using order_payment.domain.Services;
using order_payment.infrastructure.Configuration;
using order_payment.infrastructure.Persistence.Repositories;
using order_payment.infrastructure.Queue.Adapters;
using System.Data;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(AppContext.BaseDirectory);
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        // 1. Pegamos a connection string do appsettings.json
        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));

        services.AddRabbitMq(context.Configuration);
        services.AddScoped<IQueueService, RabbitMqAdapter>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddHostedService<PedidoQueueConsumer>();

 
    });

var host = builder.Build();
await host.RunAsync();