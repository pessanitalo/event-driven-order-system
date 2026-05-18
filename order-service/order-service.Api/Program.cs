using Npgsql;
using order_service.Application.Pedido.UseCases;
using order_service.Domain.Repositories;
using order_service.Infrastructure.DependencyInjection;
using order_service.Infrastructure.MessageBroker;
using order_service.Infrastructure.Persistence.Repository;
using System.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICriarPedidoUseCase, CriarPedidoUseCase>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IOutboxMessagesRepository, OutboxMessagesRepository>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));

builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddHostedService<OutboxPublisherWorker>();

builder.Services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
