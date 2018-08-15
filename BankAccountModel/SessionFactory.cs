using BankAccountModel.Persistence;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace BankAccountModel
{
    public static class SessionFactory
    {
        private static ISessionFactory _sessionFactory;

        public static ISession Create()
        {
            if (_sessionFactory == null)
                _sessionFactory = CreateSessionFactory();

            return _sessionFactory.OpenSession();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(builder => builder
                    .Server(@".\sqlexpress")
                    .Database("BankAccountModel")
                    .TrustedConnection()))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BankAccountMap>())
                .ExposeConfiguration(c =>
                {
                    SchemaUpdate schemaUpdate = new SchemaUpdate(c);
                    schemaUpdate.Execute(false, true);
                })
                .BuildSessionFactory();
        }
    }
}