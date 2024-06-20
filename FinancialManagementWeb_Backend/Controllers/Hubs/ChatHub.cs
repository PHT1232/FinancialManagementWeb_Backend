using Microsoft.AspNetCore.SignalR;

namespace TeamManagementProject_Backend.Controllers.Hubs
{
    public class ChatHub : Hub
    {

        private static List<string> users = new List<string>();

        public override async Task OnConnectedAsync()
        {
            users.Add(Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            users.Remove(Context.UserIdentifier);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
            => await Clients.All.SendAsync("TransferChartData", user, message);
    
        public async Task GetConnectedUser()
            => await Clients.All.SendAsync("connected", Context.User);
            
    }
}
