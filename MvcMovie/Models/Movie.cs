using System.ComponentModel.DataAnnotations;
namespace MvcMovie.Models;

public class Movie 
{
    public int Id{ get; set;}

    [StringLength(30,MinimumLength=3)]
    public string Title { get; set;}

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set;}
    
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "The field Genre must be started with capital letter")]
    [StringLength(20)]
    public string Genre { get; set;}

    [Range(1,100)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set;}

    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [StringLength(5)]
    public string Rating {get; set;}
}