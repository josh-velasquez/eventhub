using api.Domain.DTOs;
using api.Domain.Interfaces;
using api.Services;
using NHibernate.Linq;

namespace api.Domain.Services;

public class EventService : IEventService
{
    private readonly INHibernateService _nhibernateService;

    public EventService(INHibernateService nhibernateService)
    {
        _nhibernateService = nhibernateService;
    }

    public async Task<EventsResponse> GetEvents(EventsQuery query)
    {s
        var mockEvents = new List<EventDto>
        {
            new EventDto { Id = "1", Name = "Tech Conference 2024", StartsOn = DateTime.Now.AddDays(30), EndsOn = DateTime.Now.AddDays(32), Location = "San Francisco, CA" },
            new EventDto { Id = "2", Name = "Music Festival", StartsOn = DateTime.Now.AddDays(45), EndsOn = DateTime.Now.AddDays(47), Location = "Austin, TX" },
            new EventDto { Id = "3", Name = "Business Summit", StartsOn = DateTime.Now.AddDays(15), EndsOn = DateTime.Now.AddDays(16), Location = "New York, NY" },
            new EventDto { Id = "4", Name = "Sports Championship", StartsOn = DateTime.Now.AddDays(60), EndsOn = DateTime.Now.AddDays(62), Location = "Los Angeles, CA" },
            new EventDto { Id = "5", Name = "Art Exhibition", StartsOn = DateTime.Now.AddDays(20), EndsOn = DateTime.Now.AddDays(25), Location = "Chicago, IL" },
            new EventDto { Id = "6", Name = "Food & Wine Festival", StartsOn = DateTime.Now.AddDays(75), EndsOn = DateTime.Now.AddDays(77), Location = "Napa Valley, CA" },
            new EventDto { Id = "7", Name = "Gaming Convention", StartsOn = DateTime.Now.AddDays(90), EndsOn = DateTime.Now.AddDays(92), Location = "Las Vegas, NV" },
            new EventDto { Id = "8", Name = "Educational Workshop", StartsOn = DateTime.Now.AddDays(10), EndsOn = DateTime.Now.AddDays(10), Location = "Boston, MA" },
            new EventDto { Id = "9", Name = "Fashion Show", StartsOn = DateTime.Now.AddDays(120), EndsOn = DateTime.Now.AddDays(122), Location = "Miami, FL" },
            new EventDto { Id = "10", Name = "Science Symposium", StartsOn = DateTime.Now.AddDays(35), EndsOn = DateTime.Now.AddDays(37), Location = "Seattle, WA" }
        };

        var filteredEvents = mockEvents;
        if (!string.IsNullOrWhiteSpace(query.SearchQuery))
        {
            filteredEvents = mockEvents.Where(e => 
                e.Name.Contains(query.SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                e.Location.Contains(query.SearchQuery, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        if (!string.IsNullOrWhiteSpace(query.StartDate) && DateTime.TryParse(query.StartDate, out var startDate))
        {
            filteredEvents = filteredEvents.Where(e => e.StartsOn >= startDate).ToList();
        }

        if (!string.IsNullOrWhiteSpace(query.EndDate) && DateTime.TryParse(query.EndDate, out var endDate))
        {
            filteredEvents = filteredEvents.Where(e => e.EndsOn <= endDate).ToList();
        }

        var totalCount = filteredEvents.Count;

        var sortedEvents = query.SortBy.ToLower() switch
        {
            "name" => query.SortOrder.ToLower() == "desc" 
                ? filteredEvents.OrderByDescending(e => e.Name).ToList()
                : filteredEvents.OrderBy(e => e.Name).ToList(),
            "location" => query.SortOrder.ToLower() == "desc"
                ? filteredEvents.OrderByDescending(e => e.Location).ToList()
                : filteredEvents.OrderBy(e => e.Location).ToList(),
            "startsOn" => query.SortOrder.ToLower() == "desc"
                ? filteredEvents.OrderByDescending(e => e.StartsOn).ToList()
                : filteredEvents.OrderBy(e => e.StartsOn).ToList(),
            _ => query.SortOrder.ToLower() == "desc"
                ? filteredEvents.OrderByDescending(e => e.StartsOn).ToList()
                : filteredEvents.OrderBy(e => e.StartsOn).ToList()
        };

        var skip = (query.Page - 1) * query.PageSize;
        var pagedEvents = sortedEvents.Skip(skip).Take(query.PageSize).ToList();

        var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);
        var currentPage = Math.Max(1, Math.Min(query.Page, totalPages));

        return new EventsResponse
        {
            Events = pagedEvents,
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = currentPage,
            PageSize = query.PageSize
        };
    }
}
