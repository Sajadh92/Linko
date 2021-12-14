using System;
using System.Collections.Generic;

namespace Linko.Helper
{
    public class ConnHubRepository : IRegisterSingleton, IConnHubRepository
    {
        private static readonly Dictionary<int, HashSet<(string connId, bool phoneApp)>> userMap = new();

        public IEnumerable<int> OnlineUsers { get { return userMap.Keys; } }

        public void AddConnection(int userId, string connId, bool phoneApp)
        {
            lock (userMap)
            {
                if (!userMap.ContainsKey(userId))
                    userMap[userId] = new HashSet<(string connId, bool phoneApp)>();

                userMap[userId].Add((connId, phoneApp));
            }
        }

        public void RemoveConnection(int userId, string connId, bool phoneApp)
        {
            lock (userMap)
            {
                if (userMap[userId].Contains((connId, phoneApp)))
                    userMap[userId].Remove((connId, phoneApp));
            }
        }

        public HashSet<(string connId, bool phoneApp)> GetConnections(int userId)
        {
            try
            {
                lock (userMap)
                    return userMap[userId];
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
