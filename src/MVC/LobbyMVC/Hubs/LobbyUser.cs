using System;
using System.Collections.Generic;
using System.Linq;

namespace LobbyMVC.Hubs
{
    public class LobbyUser
    {
        private readonly List<LobbyConnection> _connections;


        public string UserName { get; }

        public LobbyUser(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            _connections = new List<LobbyConnection>();
        }

        public IEnumerable<LobbyConnection> Connections => _connections;

        public void AppendConnection(string connectionId)
        {
            if (connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));
            }

            var connection = new LobbyConnection
            {
                ConnectedAt = DateTime.UtcNow,
                ConnectionId = connectionId
            };

            

            _connections.Add(connection);
        }


        public void RemoveConnection(string connectionId)
        {
            if (connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));
            }

            var connection = _connections.SingleOrDefault(x => x.ConnectionId.Equals(connectionId));

            if (connection == null)
            {
                return;
            }

            _connections.Remove(connection);
        }


    }
}
