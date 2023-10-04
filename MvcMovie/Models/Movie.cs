using System.ComponentModel.DataAnnotations;
namespace MvcMovie.Models;

public class Movie 
{
    public int Id{ get; set;}
    public string Title { get; set;}
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set;}
    public string Genre { get; set;}

    [DataType(DataType.Currency)]
    public decimal Price { get; set;}
}