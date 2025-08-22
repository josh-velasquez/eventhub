using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Connection;
using NHibernate.Driver;
using api.Domain.Entities;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
// using System.Data.SQLite;    

namespace api.Services;

public interface INHibernateService
{
    NHibernate.ISession OpenSession();
}


public class NHibernateService : INHibernateService
{
    private readonly ISessionFactory _sessionFactory;

    public NHibernateService(IConfiguration configuration)
    {
        // var config = new Configuration();
        
        // var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "events.db");
        // var connectionString = $"Data Source={dbPath};Version=3;New=False;Compress=True;";
        
        // config.DataBaseIntegration(db =>
        // {
        //     db.ConnectionString = connectionString;
        //     db.Driver<SQLiteDataDriver>();
        //     db.Dialect<SQLiteDialect>();
        //     db.LogSqlInConsole = true;
        //     db.LogFormattedSql = true;
        //     db.ConnectionProvider<SQLiteConnectionProvider>();
        // });

        // config.SetProperty("hbm2ddl.auto", "none");
        // config.SetProperty("hibernate.temp.use_jdbc_metadata_defaults", "false");

        // config.AddAssembly(typeof(Event).Assembly);

        // _sessionFactory = config.BuildSessionFactory();
    }

    public NHibernate.ISession OpenSession()
    {
        return _sessionFactory.OpenSession();
    }
}
