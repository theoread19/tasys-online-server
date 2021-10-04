using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Token;

namespace TASysOnlineProject.Config.HubConfig
{
    public class AuthHub : Hub
    {
        [AllowAnonymous]
        public Task<string> Authorize()
        {
            return Task.Run(() => { return TokenHelper.GenerateToken(); });
        }
    }
}
