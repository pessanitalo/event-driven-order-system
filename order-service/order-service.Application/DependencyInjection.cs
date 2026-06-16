using Microsoft.Extensions.DependencyInjection;
using order_service.Application.Pedido.UseCases;

namespace order_service.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICriarPedidoUseCase, CriarPedidoUseCase>();
            services.AddScoped<IBuscarPedidosUseCase, BuscarPedidosUseCase>();
            return services;
        }
    }
}
