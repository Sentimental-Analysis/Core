using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using Core.Database.Implementations;
using Core.Database.Interfaces;
using Core.Models;

namespace Core.Builders
{
    public class CassandraConnectionBuilder : IBuilder<ICassandraConnection>
    {
        public ISession Session { get; set; }
        public IMapper Mapper { get; set; }

        public static CassandraConnectionBuilder Connection(Cluster cluster) => new CassandraConnectionBuilder(cluster);

        public CassandraConnectionBuilder(ICassandraConnection connection)
        {
            Session = connection.Session;
            Mapper = connection.Mapper;
        }

        public CassandraConnectionBuilder(Cluster cluster)
        {
            var session = cluster.ConnectAndCreateDefaultKeyspaceIfNotExists();
            session.GetTable<Tweet>().CreateIfNotExists();
            var mapper = new Mapper(session);
            Session = session;
            Mapper = mapper;
        }

        public ICassandraConnection Build()
        {
            return new DefaultCassandraConnection(Session, Mapper);
        }
    }
}