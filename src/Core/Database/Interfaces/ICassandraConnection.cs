using System;
using Cassandra;
using Cassandra.Mapping;

namespace Core.Database.Interfaces
{
    public interface ICassandraConnection : IDisposable
    {
        ISession Session { get; }
        IMapper Mapper { get; }
    }
}