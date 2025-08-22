using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using api.Domain.Entities;

namespace api.Services;

public class TicketMapping : ClassMapping<Ticket>
{
    public TicketMapping()
    {
        Table("TicketSales");
        Id(x => x.Id, map => map.Generator(Generators.Assigned));
        Property(x => x.EventId, map => map.NotNullable(true));
        Property(x => x.UserId, map => map.NotNullable(true));
        Property(x => x.PurchaseDate, map => map.NotNullable(true));
        Property(x => x.PriceInCents, map => map.NotNullable(true));
        
        ManyToOne(x => x.Event, map => 
        {
            map.Column("EventId");
            map.Insert(false);
            map.Update(false);
        });
    }
}
