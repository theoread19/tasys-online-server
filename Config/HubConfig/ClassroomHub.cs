using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;

namespace TASysOnlineProject.Config.HubConfig
{
    public class ClassroomHub : Hub
    {
        public static Dictionary<string, List<UserAccountAuthRequest>> ConnectedClients = new Dictionary<string, List<UserAccountAuthRequest>>();

    }
}
