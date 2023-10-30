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
    
    
    public string Genre { get; set;}

    [Range(1,100)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set;}

    public string Rating {get; set;}
}