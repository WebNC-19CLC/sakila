using System;
using System.Collections.Generic;

namespace sakilaAppMySQL.Infrastructure.Domain.Entities
{
  public partial class Inventory
  {
    public Inventory()
    {
      Rentals = new HashSet<Rental>();
    }

    public uint InventoryId { get; set; }
    public ushort FilmId { get; set; }
    public byte StoreId { get; set; }
    public DateTime LastUpdate { get; set; }

    public virtual Film Film { get; set; } = null!;
    public virtual Store Store { get; set; } = null!;
    public virtual ICollection<Rental> Rentals { get; set; }
  }
}
