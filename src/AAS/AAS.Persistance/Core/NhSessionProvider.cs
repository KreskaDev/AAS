using System;
using NHibernate;

namespace AAS.Persistance
{
    public class NhSessionProvider
        : IDisposable
    {
        private readonly DataBasePersister _dataBasePersister;

        private ISessionFactory _sessionFactory;
        private readonly object _syncRoot = new object();


        public NhSessionProvider(DataBasePersister dataBasePersister)
        {
            _dataBasePersister = dataBasePersister;
        }

        public ISession OpenSession()
        {
            if (_sessionFactory == null)
            {
                lock (_syncRoot)
                {
                    if (_sessionFactory == null)
                    {
                        _sessionFactory = _dataBasePersister.Configuration.BuildSessionFactory();
                    }
                }
            }

            return _sessionFactory.OpenSession();
        }

        public IStatelessSession OpenStatelessSession()
        {
            if (_sessionFactory == null)
            {
                lock (_syncRoot)
                {
                    if (_sessionFactory == null)
                        _sessionFactory = _dataBasePersister.Configuration.BuildSessionFactory();
                }
            }

            return _sessionFactory.OpenStatelessSession();
        }


        public void WithSession(Action<ISession> operation)
        {
            using (var session = OpenSession())
            {
                operation(session);
            }
        }

        public void Dispose()
        {
        }
    }
}
