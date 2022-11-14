namespace sakilaAppMySQL.Dtos.ActorsDto
{
  public class ActorDto
  {
    /// <summary>
    /// Actor's id
    /// </summary>
    public ushort ActorId { get; set; }
    /// <summary>
    /// First name of actor
    /// </summary>
    public string FirstName { get; set; } = null!;
    /// <summary>
    /// Last name of actor
    /// </summary>
    public string LastName { get; set; } = null!;
    /// <summary>
    /// Last time the actor info is updated
    /// </summary>
    public DateTime LastUpdate { get; set; }
  }
}
