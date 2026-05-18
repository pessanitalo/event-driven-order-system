using Microsoft.Extensions.Logging;
using order_service.Application.Pedido.DTOs;
using order_service.Domain.Common;
using order_service.Domain.ValueObjects;
using PedidoDomain = order_service.Domain.Pedidos.Domain.Entities.Pedido;
using ItemPedidoDomain = order_service.Domain.ItemPedido.Domain.Entities.ItemPedido;
using OutboxMessageDomain = order_service.Domain.OutboxMessages.Domain.Entities.OutboxMessage;
using order_service.Domain.Repositories;
using order_service.Infrastructure.MessageBroker;


namespace order_service.Application.Pedido.UseCases
{
    public class CriarPedidoUseCase : ICriarPedidoUseCase
    {
        protected readonly ILogger<CriarPedidoUseCase> _logger;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IOutboxMessagesRepository _outboxMessagesRepository;
        private readonly IMessagePublisher _messagePublisher;

        public CriarPedidoUseCase(ILogger<CriarPedidoUseCase> logger, IPedidoRepository pedidoRepository,
            IOutboxMessagesRepository outboxMessagesRepository, IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _pedidoRepository = pedidoRepository;
            _outboxMessagesRepository = outboxMessagesRepository;
            _messagePublisher = messagePublisher;
        }

        public async Task<Result<string>> CriarPedidoAsync(CriarPedidoRequest criarPedidoRequest)
        {
            try
            {
                var itensDomain = criarPedidoRequest.Itens
                    .Select(item => new ItemPedidoDomain(
                        item.Produto,
                        item.Quantidade,
                        item.PrecoUnitario
                    ))
                    .ToList();

                var pedido = PedidoDomain.CriarPedido(
                    pedidoId: Guid.NewGuid(),
                    nomeCliente: new NomeCliente(criarPedidoRequest.NomeCliente),
                    cpfCliente: new CPF(criarPedidoRequest.CpfCliente),
                    precoTotal: 0,
                    createAt: DateTime.UtcNow,
                    updateAt: DateTime.UtcNow,
                    idempotencyKey: criarPedidoRequest.IdempotencyKey
                );

                pedido.Itens = itensDomain;
                pedido.AtualizarTotal();

                var validationResult = await _pedidoRepository.GetByIdempotencyKey(pedido.IdempotencyKey);
                if (validationResult != null)
                {
                    return Result<string>.Fail("Já existe um pedido com a mesma chave de idempotência.");
                }

                // 1 passo: salvou o pedido no banco de dados
                await _pedidoRepository.CreatePedidoAsync(pedido);

                // cria o evento na tabela, outbox
                var OutboxMessage = new OutboxMessageDomain
                {

                    EventType = "PedidoCriadoEvent",
                    Payload = System.Text.Json.JsonSerializer.Serialize(new PedidoCriadoEvent
                    {
                        PedidoId = pedido.PedidoId,
                        NomeCliente = criarPedidoRequest.NomeCliente,
                        CpfCliente = criarPedidoRequest.CpfCliente,
                        PrecoTotal = pedido.PrecoTotal,
                        DataCriacao = DateTime.UtcNow,
                        IdempotencyKey = pedido.IdempotencyKey
                    }),
                    CreatedAt = DateTime.UtcNow,
                    ProcessedAt = null,
                    Status = "Pending",
                    RetryCount = 0,
                    ErrorMessage = null
                };
                await _outboxMessagesRepository.CreatePedidoAsync(OutboxMessage);
                return Result<string>.Ok("Pedido criado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar pedido");
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
