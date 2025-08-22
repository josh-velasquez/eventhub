using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using api.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernateSession = NHibernate.ISession;

namespace api.Services;

public interface INHibernateService
{
    NHibernateSession OpenSession();
}

public class NHibernateService : INHibernateService
{
    private readonly ISessionFactory _sessionFactory;

    public NHibernateService(IConfiguration configuration)
    {
        var config = new Configuration();
        
        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "events.db");
        var connectionString = $"Data Source={dbPath}";
        
        config.DataBaseIntegration(db =>
        {
            db.ConnectionString = connectionString;
            db.Driver<SQLite20Driver>();
            db.Dialect<SQLiteDialect>();
            db.LogSqlInConsole = true;
            db.LogFormattedSql = true;
        });

        config.SetProperty("hbm2ddl.auto", "update");
        config.SetProperty("hibernate.temp.use_jdbc_metadata_defaults", "false");

        var mapper = new ModelMapper();
        mapper.AddMapping<EventMapping>();
        mapper.AddMapping<TicketMapping>();
        
        var hbmMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        config.AddMapping(hbmMapping);

        _sessionFactory = config.BuildSessionFactory();
    }

    public NHibernateSession OpenSession()
    {
        return _sessionFactory.OpenSession();
    }
}
