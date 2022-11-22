using Microsoft.AspNetCore.SignalR;

namespace sakilaAppMySQL.Hubs
{
  public class MessageHub : Hub<IMessageHubClient>
  {
    public async Task SendNotificationToUsers(string message)
    {
      await Clients.All.SendNotificationToUsers(message);
    }
  }
}
