using Cassandra; // Đảm bảo đã có dòng này
using CassandraSession = Cassandra.ISession; // Sử dụng alias cho ISession từ Cassandra
using Project_Cassandra.Models; // Đảm bảo dòng này có trong CassandraTestController.cs

namespace Project_Cassandra.Data
{
    public class CassandraConnection
    {
        private readonly Cassandra.ISession _session; // Sử dụng alias ở đây
        private readonly Cluster cluster;

        public CassandraConnection(string contactPoint, int port, string keyspace)
        {
            cluster = Cluster.Builder()
                .AddContactPoint(contactPoint) // Sử dụng contact point từ appsettings.json
                .WithPort(port)
                .Build();

            _session = cluster.Connect(keyspace);
        }

        public Cassandra.ISession GetSession() => _session; // Sử dụng alias ở đây
    }
}
