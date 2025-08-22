namespace api.Domain.DTOs;

public class EventDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime StartsOn { get; set; }
    public DateTime EndsOn { get; set; }
    public string Location { get; set; } = string.Empty;
}

public class EventsResponse
{
    public List<EventDto> Events { get; set; } = new();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}

public class EventsQuery
{
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "startsOn";
    public string SortOrder { get; set; } = "asc";
    public string? SearchQuery { get; set; }
}
