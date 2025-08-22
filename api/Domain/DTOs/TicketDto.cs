namespace api.Domain.DTOs;

public class TicketDto
{
    public string Id { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public int PriceInCents { get; set; }
}

public class TicketsResponse
{
    public List<TicketDto> Tickets { get; set; } = new();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}

public class TicketsQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? EventId { get; set; }
    public string? UserId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string SortBy { get; set; } = "PurchaseDate";
    public string SortOrder { get; set; } = "desc";
}

public class EventSalesDto
{
    public string EventId { get; set; } = string.Empty;
    public string EventName { get; set; } = string.Empty;
    public int SalesCount { get; set; }
    public int TotalAmountInCents { get; set; }
    public decimal TotalAmountInDollars => TotalAmountInCents / 100m;
}
