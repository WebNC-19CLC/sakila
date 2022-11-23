using sakilaAppMySQL.Dtos.ActorsDto;

namespace sakilaAppMySQL.Hubs
{
  public interface IMessageHubClient
  {
    Task SendNotificationAddActorToUsers(ActorDto actor);

    Task SendNotificationDeleteActorToUsers(int Id);

    Task JoinGroupNotification();
  }
}
