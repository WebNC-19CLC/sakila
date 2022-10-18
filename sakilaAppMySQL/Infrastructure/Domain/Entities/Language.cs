using System;
using System.Collections.Generic;

namespace sakilaAppMySQL.Infrastructure.Domain.Entities
{
  public partial class Language
  {
    public Language()
    {
      FilmLanguages = new HashSet<Film>();
      FilmOriginalLanguages = new HashSet<Film>();
    }

    public byte LanguageId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime LastUpdate { get; set; }

    public virtual ICollection<Film> FilmLanguages { get; set; }
    public virtual ICollection<Film> FilmOriginalLanguages { get; set; }
  }
}
