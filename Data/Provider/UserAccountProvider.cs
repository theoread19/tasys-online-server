using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace TASysOnlineProject.Data.Provider
{
    public class UserAccountProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
