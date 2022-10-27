namespace sakilaAppMySQL.Dtos.FilmsDto
{
  public class SearchFilmFilterDto
  {
    public int NumPerPage { get; set; } = 10;
    public int PageNum { get; set; } = 1;
  }
}
