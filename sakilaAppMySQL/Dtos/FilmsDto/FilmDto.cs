namespace sakilaAppMySQL.Dtos.FilmsDto
{
  public class FilmDto
  {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public short? ReleaseYear { get; set; }
    public decimal RentalRate { get; set; }
    public ushort? Length { get; set; }
    public byte RentalDuration { get; set; }
    public decimal ReplacementCost { get; set; }
    public string? Rating { get; set; }
    public DateTime LastUpdate { get; set; }
    public string Language { get; set; } = null!;
    public string OriginalLanguage { get; set; } = null!;
    public ICollection<string> FilmActors { get; set; }
    public ICollection<string> FilmCategories { get; set; }
    public ICollection<string> Inventories { get; set; }
  }
}
