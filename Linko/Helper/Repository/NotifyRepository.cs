using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linko.Helper
{
    public class NotifyRepository : INotifyRepository, IRegisterSingleton
    {
        IHubContext<NotificationHub> HubContext { get; }

        private readonly ILoggerRepository _logger;
        private readonly IConnHubRepository _connHub;

        public NotifyRepository(
            IHubContext<NotificationHub> hubContext,
            ILoggerRepository logger,
            IConnHubRepository connHub)
        {
            HubContext = hubContext;

            _logger = logger;
            _connHub = connHub;
        }

        public async Task SendNotifications()
        {
            try
            {
                List<string> usersConnections = new();

                object[] notification = new object[1];

                foreach (int userId in _connHub.OnlineUsers)
                {
                    List<string> connections = _connHub.GetConnections(userId)
                                                       .Where(x => x.phoneApp == false)
                                                       .Select(x => x.connId)
                                                       .ToList();

                    if (connections != null && connections.Count > 0)
                        usersConnections.AddRange(connections);
                }

                try
                {
                    await HubContext.Clients.Clients(usersConnections).SendCoreAsync("brodcastNotification", notification);
                }
                catch (Exception ex)
                {
                    await _logger.WriteAsync(ex, "NotifyRepository => brodcastNotification");
                }

            }
            catch (Exception ex)
            {
                await _logger.WriteAsync(ex, "NotifyRepository => SendNotifications");
            }
        }
    }
}
