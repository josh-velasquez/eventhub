using api.Domain.DTOs;
using api.Domain.Interfaces;
using api.Domain.Entities;
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
    {
        using var session = _nhibernateService.OpenSession();
        
        var queryable = session.Query<Event>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchQuery))
        {
            queryable = queryable.Where(e => 
                e.Name.Contains(query.SearchQuery) ||
                e.Location.Contains(query.SearchQuery));
        }

        if (!string.IsNullOrWhiteSpace(query.StartDate) && DateTime.TryParse(query.StartDate, out var startDate))
        {
            queryable = queryable.Where(e => e.StartsOn >= startDate);
        }

        if (!string.IsNullOrWhiteSpace(query.EndDate) && DateTime.TryParse(query.EndDate, out var endDate))
        {
            queryable = queryable.Where(e => e.EndsOn <= endDate);
        }

        var totalCount = await queryable.CountAsync();

        queryable = query.SortBy.ToLower() switch
        {
            "name" => query.SortOrder.ToLower() == "desc" 
                ? queryable.OrderByDescending(e => e.Name)
                : queryable.OrderBy(e => e.Name),
            "location" => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(e => e.Location)
                : queryable.OrderBy(e => e.Location),
            "startsOn" => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(e => e.StartsOn)
                : queryable.OrderBy(e => e.StartsOn),
            _ => query.SortOrder.ToLower() == "desc"
                ? queryable.OrderByDescending(e => e.StartsOn)
                : queryable.OrderBy(e => e.StartsOn)
        };

        var skip = (query.Page - 1) * query.PageSize;
        var events = await queryable.Skip(skip).Take(query.PageSize).ToListAsync();

        var eventDtos = events.Select(e => new EventDto
        {
            Id = e.Id,
            Name = e.Name,
            StartsOn = e.StartsOn,
            EndsOn = e.EndsOn,
            Location = e.Location
        }).ToList();

        var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);
        var currentPage = Math.Max(1, Math.Min(query.Page, totalPages));

        return new EventsResponse
        {
            Events = eventDtos,
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = currentPage,
            PageSize = query.PageSize
        };
    }
}
