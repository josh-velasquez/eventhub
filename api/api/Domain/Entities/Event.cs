using System.ComponentModel.DataAnnotations;

namespace api.Domain.Entities;

public class Event
{
    public virtual string Id { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200)]
    public virtual string Name { get; set; } = string.Empty;
    
    [Required]
    public virtual DateTime StartsOn { get; set; }
    
    [Required]
    public virtual DateTime EndsOn { get; set; }
    
    [Required]
    [StringLength(200)]
    public virtual string Location { get; set; } = string.Empty;
}
