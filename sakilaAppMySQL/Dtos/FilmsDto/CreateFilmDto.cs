namespace sakilaAppMySQL.Dtos.FilmsDto
{
  public class CreateFilmDto
  {
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public short? ReleaseYear { get; set; }
    public decimal RentalRate { get; set; }
    public ushort? Length { get; set; }
    public byte RentalDuration { get; set; }
    public decimal ReplacementCost { get; set; }
    public string? Rating { get; set; }
    public int LanguageId { get; set; }
    public int? OriginalLanguageId { get; set; }
    public ICollection<int> FilmActorIds { get; set; } = new List<int>();
    public ICollection<int> FilmCategoriesIds { get; set; } = new List<int>();
    public ICollection<int> Inventories { get; set; } = new List<int>();
  }
}
