using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using Core.Database.Interfaces;
using Core.Models;
using Core.Models.Mappings;

namespace Core.Database.Implementations
{
    public class DefaultCassandraConnection : ICassandraConnection
    {
        public ISession Session { get; }
        public IMapper Mapper { get; }

        public DefaultCassandraConnection(ISession session, IMapper mapper)
        {
            Session = session;
            Mapper = mapper;
        }

        public static ICassandraConnection Connect(Cluster cluster)
        {
            var session = cluster.ConnectAndCreateDefaultKeyspaceIfNotExists();
            session.GetTable<Tweet>().CreateIfNotExists();           
            var mapper = new Mapper(session);
            return new DefaultCassandraConnection(session, mapper);
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