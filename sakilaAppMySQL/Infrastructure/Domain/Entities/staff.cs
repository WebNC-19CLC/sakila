using System;
using System.Collections.Generic;

namespace sakilaAppMySQL.Infrastructure.Domain.Entities
{
  public partial class staff
  {
    public staff()
    {
      Payments = new HashSet<Payment>();
      Rentals = new HashSet<Rental>();
    }

    public byte StaffId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ushort AddressId { get; set; }
    public byte[]? Picture { get; set; }
    public string? Email { get; set; }
    public byte StoreId { get; set; }
    public bool? Active { get; set; }
    public string Username { get; set; } = null!;
    public string? Password { get; set; }
    public DateTime LastUpdate { get; set; }

    public virtual Address Address { get; set; } = null!;
    public virtual Store StoreNavigation { get; set; } = null!;
    public virtual Store? Store { get; set; }
    public virtual ICollection<Payment> Payments { get; set; }
    public virtual ICollection<Rental> Rentals { get; set; }
  }
}
