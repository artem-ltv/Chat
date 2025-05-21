using Chat.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hubs
{
    public interface IChatClient
    {
        public Task ReceiveMessage(string userName, string message);
    }

    public class ChatHub : Hub<IChatClient>
    {
        public async Task Join(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.ChatRoom);

            await Clients
                .Group(userConnection.ChatRoom)
                .ReceiveMessage("Admin", $"{userConnection.UserName} connected to chat.");
        }
    }
}
