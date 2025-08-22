using api.Domain.DTOs;

namespace api.Domain.Interfaces;

public interface IEventService
{
    Task<EventsResponse> GetEvents(EventsQuery query);
}
