using Microsoft.AspNetCore.Identity;

namespace sakilaAppMySQL.Infrastructure.Domain.Entities.Authentication
{
  public class ApplicationUser : IdentityUser
  {
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
  }
}
