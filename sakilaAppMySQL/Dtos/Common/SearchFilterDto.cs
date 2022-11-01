using System.ComponentModel.DataAnnotations;

namespace sakilaAppMySQL.Dtos.Common
{
  public class SearchFilterDto
  {
    [Required]
    [Range(0, 30)]
    public int NumPerPage { get; set; } = 10;
    [Required]
    [Range(0, int.MaxValue)]
    public int PageNum { get; set; } = 1;
  }
}
