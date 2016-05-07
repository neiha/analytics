using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using analytics.Models.DAO.Mapping;
using NHibernate;
using NHibernate.Cfg;
using System.Web;

namespace analytics.Models.DAO
{
    public sealed class NHibernateHelper
    {
        private static readonly HttpContext _httpContextAccessor;
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static ISessionFactory sessionFactory;

        static NHibernateHelper()
        {
            Configuration config = new Configuration();
            _httpContextAccessor= HttpContext.Current;
            // FluentNHibernate Configuration API for configuring NHibernate
            config = Fluently.Configure(config)
                .Database(
                        MsSqlConfiguration.MsSql2012
                            .ConnectionString("Server=DESKTOP-E6AUF6I; Database =analytics; Uid = sa; Pwd =hola;")
                            .UseReflectionOptimizer()
                            .AdoNetBatchSize(100))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TiempoMap>())
                .BuildConfiguration();
            sessionFactory = config.BuildSessionFactory();
/*
            Configuration config = new Configuration().DataBaseIntegration(db =>
            {
                db.ConnectionString = "Server=127.0.0.1;Database=menudehoy;Uid=root;Pwd=;";
                db.Dialect<MySQLDialect>();
            }).SetProperty("nhibernate.show_sql", "true").Configure();
                    
            config.AddAssembly("fonditas");
            sessionFactory = config.BuildSessionFactory();*/
        }
        public static ISession GetCurrentSession()
        {
            ISession currentSession = _httpContextAccessor.Items[CurrentSessionKey] as ISession;

            if (currentSession == null || !currentSession.IsOpen)
            {
                currentSession = sessionFactory.OpenSession();
                _httpContextAccessor.Items[CurrentSessionKey] = currentSession;
            }

            return currentSession;
        }

        public static void CloseSession()
        {

            ISession currentSession = _httpContextAccessor.Items[CurrentSessionKey] as ISession;

            if (currentSession == null)
                return;

            currentSession.Close();
            _httpContextAccessor.Items.Remove(CurrentSessionKey);
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
                sessionFactory.Close();
        }
    }
}
