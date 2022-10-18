using System;
using System.Collections.Generic;

namespace sakilaAppMySQL.Infrastructure.Domain.Entities
{
  public partial class Store
  {
    public Store()
    {
      Customers = new HashSet<Customer>();
      Inventories = new HashSet<Inventory>();
      staff = new HashSet<staff>();
    }

    public byte StoreId { get; set; }
    public byte ManagerStaffId { get; set; }
    public ushort AddressId { get; set; }
    public DateTime LastUpdate { get; set; }

    public virtual Address Address { get; set; } = null!;
    public virtual staff ManagerStaff { get; set; } = null!;
    public virtual ICollection<Customer> Customers { get; set; }
    public virtual ICollection<Inventory> Inventories { get; set; }
    public virtual ICollection<staff> staff { get; set; }
  }
}
