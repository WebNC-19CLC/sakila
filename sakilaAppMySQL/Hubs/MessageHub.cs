using Microsoft.AspNetCore.SignalR;
using sakilaAppMySQL.Dtos.ActorsDto;

namespace sakilaAppMySQL.Hubs
{
  public class MessageHub : Hub<IMessageHubClient>
  {
    public async Task SendNotificationAddActorToUsers(ActorDto actor)
    {
      await Clients.Group("Notification").SendNotificationAddActorToUsers(actor);
    }

    public async Task SendNotificationDeleteActorToUsers(int Id) {
      await Clients.Group("Notification").SendNotificationDeleteActorToUsers(Id);
    }

    public async Task JoinGroupNotification() {
      await Groups.AddToGroupAsync(Context.ConnectionId, "Notification");
    }
  }
}
