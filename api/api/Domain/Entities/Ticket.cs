using System.ComponentModel.DataAnnotations;

namespace api.Domain.Entities;

public class Ticket
{
    public virtual string Id { get; set; } = string.Empty;
    
    [Required]
    public virtual string EventId { get; set; } = string.Empty;
    
    [Required]
    public virtual string UserId { get; set; } = string.Empty;
    
    [Required]
    public virtual DateTime PurchaseDate { get; set; }
    
    [Required]
    public virtual int PriceInCents { get; set; }
    
    public virtual Event? Event { get; set; }
}