namespace sakilaAppMySQL.Dtos
{
  public class ActorDto
  {
    public ushort ActorId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime LastUpdate { get; set; }
  }
}
