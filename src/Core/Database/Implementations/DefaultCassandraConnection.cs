using Cassandra;
using Cassandra.Mapping;
using Core.Builders;
using Core.Database.Interfaces;

namespace Core.Database.Implementations
{
    public class DefaultCassandraConnection : ICassandraConnection
    {
        public ISession Session { get; }
        public IMapper Mapper { get; }

        public CassandraConnectionBuilder Builder => new CassandraConnectionBuilder(this);

        public DefaultCassandraConnection(ISession session, IMapper mapper)
        {
            Session = session;
            Mapper = mapper;
        }

        public void Dispose()
        {
            if (!Session.IsDisposed)
            {
                Session.Dispose();
            }
        }
    }
}