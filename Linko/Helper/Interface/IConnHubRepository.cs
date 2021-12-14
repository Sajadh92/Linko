using System.Collections.Generic;

namespace Linko.Helper
{
    public interface IConnHubRepository
    {
        void AddConnection(int userId, string connId, bool phoneApp);
        void RemoveConnection(int userId, string connId, bool phoneApp);
        HashSet<(string connId, bool phoneApp)> GetConnections(int userId);
        IEnumerable<int> OnlineUsers { get; }
    }
}
