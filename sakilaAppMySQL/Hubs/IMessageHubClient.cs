namespace sakilaAppMySQL.Hubs
{
  public interface IMessageHubClient
  {
    Task SendNotificationToUsers(string message);
  }
}
