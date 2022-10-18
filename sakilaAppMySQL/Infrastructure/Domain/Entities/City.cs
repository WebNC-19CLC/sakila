using System;
using System.Collections.Generic;

namespace sakilaAppMySQL.Infrastructure.Domain.Entities
{
  public partial class City
  {
    public City()
    {
      Addresses = new HashSet<Address>();
    }

    public ushort CityId { get; set; }
    public string City1 { get; set; } = null!;
    public ushort CountryId { get; set; }
    public DateTime LastUpdate { get; set; }

    public virtual Country Country { get; set; } = null!;
    public virtual ICollection<Address> Addresses { get; set; }
  }
}
