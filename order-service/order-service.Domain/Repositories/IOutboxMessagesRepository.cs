using order_service.Domain.OutboxMessages.Domain.Entities;

namespace order_service.Domain.Repositories
{
    public interface IOutboxMessagesRepository
    {
        Task CreatePedidoAsync(OutboxMessage outboxMessage);
        Task<IList<OutboxMessage>> GetPendingMessagesAsync();
        Task UpdateAsync(OutboxMessage message);
    }
}
