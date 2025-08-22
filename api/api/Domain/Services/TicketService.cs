using api.Domain.DTOs;
using api.Domain.Interfaces;
using api.Domain.Entities;
using api.Services;
using NHibernate.Linq;
using System.Linq;

namespace api.Domain.Services;

public class TicketService : ITicketService
{
    private readonly INHibernateService _nhibernateService;

    public TicketService(INHibernateService nhibernateService)
    {
        _nhibernateService = nhibernateService;
    }

    public async Task<TicketsResponse> GetTickets(TicketsQuery query)
    {
        using var session = _nhibernateService.OpenSession();
        
        var queryable = session.Query<Ticket>().AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query.EventId))
        {
            queryable = queryable.Where(t => t.EventId == query.EventId);
        }

        if (!string.IsNullOrWhiteSpace(query.UserId))
        {
            queryable = queryable.Where(t => t.UserId == query.UserId);
        }

        if (!string.IsNullOrWhiteSpace(query.StartDate) && DateTime.TryParse(query.StartDate, out var startDate))
        {
            queryable = queryable.Where(t => t.PurchaseDate >= startDate);
        }

        if (!string.IsNullOrWhiteSpace(query.EndDate) && DateTime.TryParse(query.EndDate, out var endDate))
        {
            queryable = queryable.Where(t => t.PurchaseDate <= endDate);
        }

        var totalCount = await queryable.CountAsync();

        queryable = query.SortBy.ToLower() switch
        {
            "eventid" => query.SortOrder.ToLower() == "desc" 
                ? queryable.OrderByDescending(t => t.EventId)
                : queryable.OrderBy(t => t.EventId),
            "userid" => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(t => t.UserId)
                : queryable.OrderBy(t => t.UserId),
            "purchasedate" => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(t => t.PurchaseDate)
                : queryable.OrderBy(t => t.PurchaseDate),
            "priceincents" => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(t => t.PriceInCents)
                : queryable.OrderBy(t => t.PriceInCents),
            _ => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(t => t.PurchaseDate)
                : queryable.OrderBy(t => t.PurchaseDate)
        };

        var skip = (query.Page - 1) * query.PageSize;
        var tickets = await queryable.Skip(skip).Take(query.PageSize).ToListAsync();

        var ticketDtos = tickets.Select(t => new TicketDto
        {
            Id = t.Id,
            EventId = t.EventId,
            UserId = t.UserId,
            PurchaseDate = t.PurchaseDate,
            PriceInCents = t.PriceInCents
        }).ToList();

        var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);
        var currentPage = Math.Max(1, Math.Min(query.Page, totalPages));

        return new TicketsResponse
        {
            Tickets = ticketDtos,
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = currentPage,
            PageSize = query.PageSize
        };
    }

    public async Task<TicketsResponse> GetTicketsByEventId(string eventId, TicketsQuery query)
    {
        using var session = _nhibernateService.OpenSession();
        
        var queryable = session.Query<Ticket>().Where(t => t.EventId == eventId);
        
        if (!string.IsNullOrWhiteSpace(query.UserId))
        {
            queryable = queryable.Where(t => t.UserId == query.UserId);
        }

        if (!string.IsNullOrWhiteSpace(query.StartDate) && DateTime.TryParse(query.StartDate, out var startDate))
        {
            queryable = queryable.Where(t => t.PurchaseDate >= startDate);
        }

        if (!string.IsNullOrWhiteSpace(query.EndDate) && DateTime.TryParse(query.EndDate, out var endDate))
        {
            queryable = queryable.Where(t => t.PurchaseDate <= endDate);
        }

        var totalCount = await queryable.CountAsync();

        queryable = query.SortBy.ToLower() switch
        {
            "userid" => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(t => t.UserId)
                : queryable.OrderBy(t => t.UserId),
            "purchasedate" => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(t => t.PurchaseDate)
                : queryable.OrderBy(t => t.PurchaseDate),
            "priceincents" => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(t => t.PriceInCents)
                : queryable.OrderBy(t => t.PriceInCents),
            _ => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(t => t.PurchaseDate)
                : queryable.OrderBy(t => t.PurchaseDate)
        };

        var skip = (query.Page - 1) * query.PageSize;
        var tickets = await queryable.Skip(skip).Take(query.PageSize).ToListAsync();
        
        var ticketDtos = tickets.Select(t => new TicketDto
        {
            Id = t.Id,
            EventId = t.EventId,
            UserId = t.UserId,
            PurchaseDate = t.PurchaseDate,
            PriceInCents = t.PriceInCents
        }).ToList();

        var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);
        var currentPage = Math.Max(1, Math.Min(query.Page, totalPages));

        return new TicketsResponse
        {
            Tickets = ticketDtos,
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = currentPage,
            PageSize = query.PageSize
        };
    }

    public async Task<List<EventSalesDto>> GetTopEventsBySalesCount(int limit)
    {
        using var session = _nhibernateService.OpenSession();
        
        var ticketCount = await session.Query<Ticket>().CountAsync();
        var eventCount = await session.Query<Event>().CountAsync();
        
        if (ticketCount == 0 || eventCount == 0)
        {
            return new List<EventSalesDto>();
        }
        
        var eventSales = await session.Query<Ticket>()
            .Join(session.Query<Event>(), 
                  ticket => ticket.EventId, 
                  eventEntity => eventEntity.Id, 
                  (ticket, eventEntity) => new { ticket, eventEntity })
            .GroupBy(x => new { x.ticket.EventId, x.eventEntity.Name })
            .Select(g => new EventSalesDto
            {
                EventId = g.Key.EventId,
                EventName = g.Key.Name,
                SalesCount = g.Count(),
                TotalAmountInCents = g.Sum(x => x.ticket.PriceInCents)
            })
            .OrderByDescending(x => x.SalesCount)
            .Take(limit)
            .ToListAsync();

        return eventSales;
    }

    public async Task<List<EventSalesDto>> GetTopEventsByTotalAmount(int limit)
    {
        using var session = _nhibernateService.OpenSession();
        
        var ticketCount = await session.Query<Ticket>().CountAsync();
        var eventCount = await session.Query<Event>().CountAsync();
        
        if (ticketCount == 0 || eventCount == 0)
        {
            return new List<EventSalesDto>();
        }
        
        var eventSales = await session.Query<Ticket>()
            .Join(session.Query<Event>(), 
                  ticket => ticket.EventId, 
                  eventEntity => eventEntity.Id, 
                  (ticket, eventEntity) => new { ticket, eventEntity })
            .GroupBy(x => new { x.ticket.EventId, x.eventEntity.Name })
            .Select(g => new EventSalesDto
            {
                EventId = g.Key.EventId,
                EventName = g.Key.Name,
                SalesCount = g.Count(),
                TotalAmountInCents = g.Sum(x => x.ticket.PriceInCents)
            })
            .OrderByDescending(x => x.TotalAmountInCents)
            .Take(limit)
            .ToListAsync();

        return eventSales;
    }
}
