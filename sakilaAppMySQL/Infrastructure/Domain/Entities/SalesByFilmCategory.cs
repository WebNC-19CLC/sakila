﻿using System;
using System.Collections.Generic;

namespace sakilaAppMySQL.Infrastructure.Domain.Entities
{
  public partial class SalesByFilmCategory
  {
    public string Category { get; set; } = null!;
    public decimal? TotalSales { get; set; }
  }
}
