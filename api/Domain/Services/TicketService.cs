using api.Domain.DTOs;
using api.Domain.Interfaces;
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
        
        var mockTickets = new List<TicketDto>
        {
            new TicketDto { Id = "1", EventId = "1", UserId = "user1", PurchaseDate = DateTime.Now.AddDays(-30), PriceInCents = 5000 },
            new TicketDto { Id = "2", EventId = "1", UserId = "user2", PurchaseDate = DateTime.Now.AddDays(-29), PriceInCents = 5000 },
            new TicketDto { Id = "3", EventId = "1", UserId = "user3", PurchaseDate = DateTime.Now.AddDays(-28), PriceInCents = 5000 },
            new TicketDto { Id = "4", EventId = "2", UserId = "user4", PurchaseDate = DateTime.Now.AddDays(-25), PriceInCents = 7500 },
            new TicketDto { Id = "5", EventId = "2", UserId = "user5", PurchaseDate = DateTime.Now.AddDays(-24), PriceInCents = 7500 },
            new TicketDto { Id = "6", EventId = "3", UserId = "user6", PurchaseDate = DateTime.Now.AddDays(-20), PriceInCents = 10000 },
            new TicketDto { Id = "7", EventId = "4", UserId = "user7", PurchaseDate = DateTime.Now.AddDays(-15), PriceInCents = 15000 },
            new TicketDto { Id = "8", EventId = "4", UserId = "user8", PurchaseDate = DateTime.Now.AddDays(-14), PriceInCents = 15000 },
            new TicketDto { Id = "9", EventId = "5", UserId = "user9", PurchaseDate = DateTime.Now.AddDays(-10), PriceInCents = 2500 },
            new TicketDto { Id = "10", EventId = "6", UserId = "user10", PurchaseDate = DateTime.Now.AddDays(-5), PriceInCents = 12000 },
            new TicketDto { Id = "11", EventId = "6", UserId = "user11", PurchaseDate = DateTime.Now.AddDays(-4), PriceInCents = 12000 },
            new TicketDto { Id = "12", EventId = "7", UserId = "user12", PurchaseDate = DateTime.Now.AddDays(-2), PriceInCents = 8000 },
            new TicketDto { Id = "13", EventId = "8", UserId = "user13", PurchaseDate = DateTime.Now.AddDays(-1), PriceInCents = 3000 },
            new TicketDto { Id = "14", EventId = "9", UserId = "user14", PurchaseDate = DateTime.Now, PriceInCents = 20000 },
            new TicketDto { Id = "15", EventId = "10", UserId = "user15", PurchaseDate = DateTime.Now, PriceInCents = 6000 }
        };

        
        var filteredTickets = mockTickets;
        
        if (!string.IsNullOrWhiteSpace(query.EventId))
        {
            filteredTickets = filteredTickets.Where(t => t.EventId == query.EventId).ToList();
        }

        if (!string.IsNullOrWhiteSpace(query.UserId))
        {
            filteredTickets = filteredTickets.Where(t => t.UserId == query.UserId).ToList();
        }

        if (!string.IsNullOrWhiteSpace(query.StartDate) && DateTime.TryParse(query.StartDate, out var startDate))
        {
            filteredTickets = filteredTickets.Where(t => t.PurchaseDate >= startDate).ToList();
        }

        if (!string.IsNullOrWhiteSpace(query.EndDate) && DateTime.TryParse(query.EndDate, out var endDate))
        {
            filteredTickets = filteredTickets.Where(t => t.PurchaseDate <= endDate).ToList();
        }

        var totalCount = filteredTickets.Count;

        s
        var sortedTickets = query.SortBy.ToLower() switch
        {
            "eventid" => query.SortOrder.ToLower() == "desc" 
                ? filteredTickets.OrderByDescending(t => t.EventId).ToList()
                : filteredTickets.OrderBy(t => t.EventId).ToList(),
            "userid" => query.SortOrder.ToLower() == "desc"
                ? filteredTickets.OrderByDescending(t => t.UserId).ToList()
                : filteredTickets.OrderBy(t => t.UserId).ToList(),
            "purchasedate" => query.SortOrder.ToLower() == "desc"
                ? filteredTickets.OrderByDescending(t => t.PurchaseDate).ToList()
                : filteredTickets.OrderBy(t => t.PurchaseDate).ToList(),
            "priceincents" => query.SortOrder.ToLower() == "desc"
                ? filteredTickets.OrderByDescending(t => t.PriceInCents).ToList()
                : filteredTickets.OrderBy(t => t.PriceInCents).ToList(),
            _ => query.SortOrder.ToLower() == "desc"
                ? filteredTickets.OrderByDescending(t => t.PurchaseDate).ToList()
                : filteredTickets.OrderBy(t => t.PurchaseDate).ToList()
        };

        var skip = (query.Page - 1) * query.PageSize;
        var pagedTickets = sortedTickets.Skip(skip).Take(query.PageSize).ToList();

        var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);
        var currentPage = Math.Max(1, Math.Min(query.Page, totalPages));

        return new TicketsResponse
        {
            Tickets = pagedTickets,
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = currentPage,
            PageSize = query.PageSize
        };
    }

    public async Task<TicketsResponse> GetTicketsByEventId(string eventId, TicketsQuery query)
    {
        
        var mockTickets = new List<TicketDto>
        {
            new TicketDto { Id = "1", EventId = "1", UserId = "user1", PurchaseDate = DateTime.Now.AddDays(-30), PriceInCents = 5000 },
            new TicketDto { Id = "2", EventId = "1", UserId = "user2", PurchaseDate = DateTime.Now.AddDays(-29), PriceInCents = 5000 },
            new TicketDto { Id = "3", EventId = "1", UserId = "user3", PurchaseDate = DateTime.Now.AddDays(-28), PriceInCents = 5000 },
            new TicketDto { Id = "4", EventId = "2", UserId = "user4", PurchaseDate = DateTime.Now.AddDays(-25), PriceInCents = 7500 },
            new TicketDto { Id = "5", EventId = "2", UserId = "user5", PurchaseDate = DateTime.Now.AddDays(-24), PriceInCents = 7500 },
            new TicketDto { Id = "6", EventId = "3", UserId = "user6", PurchaseDate = DateTime.Now.AddDays(-20), PriceInCents = 10000 },
            new TicketDto { Id = "7", EventId = "4", UserId = "user7", PurchaseDate = DateTime.Now.AddDays(-15), PriceInCents = 15000 },
            new TicketDto { Id = "8", EventId = "4", UserId = "user8", PurchaseDate = DateTime.Now.AddDays(-14), PriceInCents = 15000 },
            new TicketDto { Id = "9", EventId = "5", UserId = "user9", PurchaseDate = DateTime.Now.AddDays(-10), PriceInCents = 2500 },
            new TicketDto { Id = "10", EventId = "6", UserId = "user10", PurchaseDate = DateTime.Now.AddDays(-5), PriceInCents = 12000 },
            new TicketDto { Id = "11", EventId = "6", UserId = "user11", PurchaseDate = DateTime.Now.AddDays(-4), PriceInCents = 12000 },
            new TicketDto { Id = "12", EventId = "7", UserId = "user12", PurchaseDate = DateTime.Now.AddDays(-2), PriceInCents = 8000 },
            new TicketDto { Id = "13", EventId = "8", UserId = "user13", PurchaseDate = DateTime.Now.AddDays(-1), PriceInCents = 3000 },
            new TicketDto { Id = "14", EventId = "9", UserId = "user14", PurchaseDate = DateTime.Now, PriceInCents = 20000 },
            new TicketDto { Id = "15", EventId = "10", UserId = "user15", PurchaseDate = DateTime.Now, PriceInCents = 6000 }
        };

        
        var filteredTickets = mockTickets.Where(t => t.EventId == eventId).ToList();
        
        if (!string.IsNullOrWhiteSpace(query.UserId))
        {
            filteredTickets = filteredTickets.Where(t => t.UserId == query.UserId).ToList();
        }

        if (!string.IsNullOrWhiteSpace(query.StartDate) && DateTime.TryParse(query.StartDate, out var startDate))
        {
            filteredTickets = filteredTickets.Where(t => t.PurchaseDate >= startDate).ToList();
        }

        if (!string.IsNullOrWhiteSpace(query.EndDate) && DateTime.TryParse(query.EndDate, out var endDate))
        {
            filteredTickets = filteredTickets.Where(t => t.PurchaseDate <= endDate).ToList();
        }

        var totalCount = filteredTickets.Count;

        var sortedTickets = query.SortBy.ToLower() switch
        {
            "userid" => query.SortOrder.ToLower() == "desc"
                ? filteredTickets.OrderByDescending(t => t.UserId).ToList()
                : filteredTickets.OrderBy(t => t.UserId).ToList(),
            "purchasedate" => query.SortOrder.ToLower() == "desc"
                ? filteredTickets.OrderByDescending(t => t.PurchaseDate).ToList()
                : filteredTickets.OrderBy(t => t.PurchaseDate).ToList(),
            "priceincents" => query.SortOrder.ToLower() == "desc"
                ? filteredTickets.OrderByDescending(t => t.PriceInCents).ToList()
                : filteredTickets.OrderBy(t => t.PriceInCents).ToList(),
            _ => query.SortOrder.ToLower() == "desc"
                ? filteredTickets.OrderByDescending(t => t.PurchaseDate).ToList()
                : filteredTickets.OrderBy(t => t.PurchaseDate).ToList()
        };


        var skip = (query.Page - 1) * query.PageSize;
        var pagedTickets = sortedTickets.Skip(skip).Take(query.PageSize).ToList();

        var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);
        var currentPage = Math.Max(1, Math.Min(query.Page, totalPages));

        return new TicketsResponse
        {
            Tickets = pagedTickets,
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = currentPage,
            PageSize = query.PageSize
        };
    }

    public async Task<List<EventSalesDto>> GetTopEventsBySalesCount(int limit)
    {
        
        var mockEventSales = new List<EventSalesDto>
        {
            new EventSalesDto { EventId = "1", EventName = "Tech Conference 2024", SalesCount = 3, TotalAmountInCents = 15000 },
            new EventSalesDto { EventId = "2", EventName = "Music Festival", SalesCount = 2, TotalAmountInCents = 15000 },
            new EventSalesDto { EventId = "4", EventName = "Sports Championship", SalesCount = 2, TotalAmountInCents = 30000 },
            new EventSalesDto { EventId = "6", EventName = "Food & Wine Festival", SalesCount = 2, TotalAmountInCents = 24000 },
            new EventSalesDto { EventId = "3", EventName = "Business Summit", SalesCount = 1, TotalAmountInCents = 10000 },
            new EventSalesDto { EventId = "5", EventName = "Art Exhibition", SalesCount = 1, TotalAmountInCents = 2500 },
            new EventSalesDto { EventId = "7", EventName = "Gaming Convention", SalesCount = 1, TotalAmountInCents = 8000 },
            new EventSalesDto { EventId = "8", EventName = "Educational Workshop", SalesCount = 1, TotalAmountInCents = 3000 },
            new EventSalesDto { EventId = "9", EventName = "Fashion Show", SalesCount = 1, TotalAmountInCents = 20000 },
            new EventSalesDto { EventId = "10", EventName = "Science Symposium", SalesCount = 1, TotalAmountInCents = 6000 }
        };

        return mockEventSales
            .OrderByDescending(x => x.SalesCount)
            .Take(limit)
            .ToList();
    }

    public async Task<List<EventSalesDto>> GetTopEventsByTotalAmount(int limit)
    {
        var mockEventSales = new List<EventSalesDto>
        {
            new EventSalesDto { EventId = "1", EventName = "Tech Conference 2024", SalesCount = 3, TotalAmountInCents = 15000 },
            new EventSalesDto { EventId = "2", EventName = "Music Festival", SalesCount = 2, TotalAmountInCents = 15000 },
            new EventSalesDto { EventId = "4", EventName = "Sports Championship", SalesCount = 2, TotalAmountInCents = 30000 },
            new EventSalesDto { EventId = "6", EventName = "Food & Wine Festival", SalesCount = 2, TotalAmountInCents = 24000 },
            new EventSalesDto { EventId = "3", EventName = "Business Summit", SalesCount = 1, TotalAmountInCents = 10000 },
            new EventSalesDto { EventId = "5", EventName = "Art Exhibition", SalesCount = 1, TotalAmountInCents = 2500 },
            new EventSalesDto { EventId = "7", EventName = "Gaming Convention", SalesCount = 1, TotalAmountInCents = 8000 },
            new EventSalesDto { EventId = "8", EventName = "Educational Workshop", SalesCount = 1, TotalAmountInCents = 3000 },
            new EventSalesDto { EventId = "9", EventName = "Fashion Show", SalesCount = 1, TotalAmountInCents = 20000 },
            new EventSalesDto { EventId = "10", EventName = "Science Symposium", SalesCount = 1, TotalAmountInCents = 6000 }
        };

        return mockEventSales
            .OrderByDescending(x => x.TotalAmountInCents)
            .Take(limit)
            .ToList();
    }
}
