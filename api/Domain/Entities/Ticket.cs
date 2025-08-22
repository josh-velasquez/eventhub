namespace api.Domain.Entities;

public class Ticket
{
    public virtual string Id { get; set; } = string.Empty;
    public virtual string EventId { get; set; } = string.Empty;
    public virtual string UserId { get; set; } = string.Empty;
    public virtual DateTime PurchaseDate { get; set; }
    public virtual int PriceInCents { get; set; }
}
