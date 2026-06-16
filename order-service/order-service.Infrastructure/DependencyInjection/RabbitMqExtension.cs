using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using order_service.Infrastructure.Configuration;
using RabbitMQ.Client;

namespace order_service.Infrastructure.DependencyInjection
{
    public static class RabbitMqExtension
    {
        public static IServiceCollection AddRabbitMq(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<RabbitMqSettings>(
                configuration.GetSection("RabbitMq"));

            services.AddSingleton<IConnection>(sp =>
            {
                var settings = sp
                    .GetRequiredService<IOptions<RabbitMqSettings>>()
                    .Value;

                var factory = new ConnectionFactory
                {
                    HostName = settings.Host,
                    Port = settings.Port,
                    UserName = settings.Username,
                    Password = settings.Password,
                    VirtualHost = settings.VirtualHost
                };

                return factory.CreateConnectionAsync().Result;
            });

            services.AddSingleton<IChannel>(sp =>
            {
                var connection = sp.GetRequiredService<IConnection>();
                var channel = connection.CreateChannelAsync().Result;

                channel.QueueDeclareAsync(
                    queue: "fila-pedidos",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null).Wait();

                return channel;
            });

            return services;
        }
    }
}