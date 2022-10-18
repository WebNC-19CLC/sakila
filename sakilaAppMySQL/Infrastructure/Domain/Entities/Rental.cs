using System;
using System.Collections.Generic;

namespace sakilaAppMySQL.Infrastructure.Domain.Entities
{
  public partial class Rental
  {
    public Rental()
    {
      Payments = new HashSet<Payment>();
    }

    public int RentalId { get; set; }
    public DateTime RentalDate { get; set; }
    public uint InventoryId { get; set; }
    public ushort CustomerId { get; set; }
    public DateTime? ReturnDate { get; set; }
    public byte StaffId { get; set; }
    public DateTime LastUpdate { get; set; }

    public virtual Customer Customer { get; set; } = null!;
    public virtual Inventory Inventory { get; set; } = null!;
    public virtual staff Staff { get; set; } = null!;
    public virtual ICollection<Payment> Payments { get; set; }
  }
}
