using System.ComponentModel.DataAnnotations;

namespace sakilaAppMySQL.Dtos.FilmsDto
{
  public class SearchFilmFilterDto
  {
    [Required]
    [Range(0,30)]
    public int NumPerPage { get; set; } = 10; 
    [Required]
    [Range(0, int.MaxValue)]
    public int PageNum { get; set; } = 1;
  }
}
