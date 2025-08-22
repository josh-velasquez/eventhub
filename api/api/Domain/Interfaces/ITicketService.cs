using api.Domain.DTOs;

namespace api.Domain.Interfaces;

public interface ITicketService
{
    Task<TicketsResponse> GetTickets(TicketsQuery query);
    Task<TicketsResponse> GetTicketsByEventId(string eventId, TicketsQuery query);
    Task<List<EventSalesDto>> GetTopEventsBySalesCount(int limit);
    Task<List<EventSalesDto>> GetTopEventsByTotalAmount(int limit);
}
