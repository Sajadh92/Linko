using Linko.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Linq;

namespace Linko.Helper
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly IConnHubRepository _connHub;

        public NotificationHub(IConnHubRepository connHub)
        {
            _connHub = connHub;
        }

        public string StartConnection()
        {
            _connHub.AddConnection(GetUserId(), Context.ConnectionId, false);

            return Context.ConnectionId;
        }

        public void EndConnection(string ConnectionId)
        {
            _connHub.RemoveConnection(GetUserId(), ConnectionId, false);
        }

        private int GetUserId()
        {
            string UserInfo = Context.User.Claims
                .Where(x => x.Type == ClaimInfo.UserManager)
                .FirstOrDefault().Value;

            if (!string.IsNullOrWhiteSpace(UserInfo))
            {
                return JsonConvert.DeserializeObject<UserManager>(UserInfo).Id;
            }

            return -1;
        }
    }
}
