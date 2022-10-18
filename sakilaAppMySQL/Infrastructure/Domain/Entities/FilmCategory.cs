﻿using System;
using System.Collections.Generic;

namespace sakilaAppMySQL.Infrastructure.Domain.Entities
{
  public partial class FilmCategory
  {
    public ushort FilmId { get; set; }
    public byte CategoryId { get; set; }
    public DateTime LastUpdate { get; set; }

    public virtual Category Category { get; set; } = null!;
    public virtual Film Film { get; set; } = null!;
  }
}
