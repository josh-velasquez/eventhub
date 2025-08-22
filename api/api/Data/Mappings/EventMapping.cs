using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using api.Domain.Entities;

namespace api.Services;

public class EventMapping : ClassMapping<Event>
{
    public EventMapping()
    {
        Table("Events");
        Id(x => x.Id, map => map.Generator(Generators.Assigned));
        Property(x => x.Name, map => 
        {
            map.NotNullable(true);
            map.Length(200);
        });
        Property(x => x.Location, map => 
        {
            map.NotNullable(true);
            map.Length(200);
        });
        Property(x => x.StartsOn, map => map.NotNullable(true));
        Property(x => x.EndsOn, map => map.NotNullable(true));
    }
}
