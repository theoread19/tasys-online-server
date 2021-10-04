using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Config.HubConfig
{
    public class ChatHub : Hub
    {
        private readonly IChatMessage _chatMessage;
        private readonly IMessageService _messageService;

        public ChatHub(IChatMessage chatMessage, IMessageService messageService)
        {
            this._chatMessage = chatMessage;
            this._messageService = messageService;
        }
        public async Task SendMessage(MessageRequest messageRequest)
        {
            //await this._messageService.CreateMessageAsync(messageRequest);
            //var chat = 
            await Clients.All.SendAsync("MessageReceived", messageRequest);
        }
    }
}
