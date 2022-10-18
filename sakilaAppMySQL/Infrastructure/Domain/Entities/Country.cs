using System;
using System.Collections.Generic;

namespace sakilaAppMySQL.Infrastructure.Domain.Entities
{
  public partial class Country
  {
    public Country()
    {
      Cities = new HashSet<City>();
    }

    public ushort CountryId { get; set; }
    public string Country1 { get; set; } = null!;
    public DateTime LastUpdate { get; set; }

    public virtual ICollection<City> Cities { get; set; }
  }
}
