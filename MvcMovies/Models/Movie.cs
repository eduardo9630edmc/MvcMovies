using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MvcMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Display(Name ="Título")]
        public string? Title { get; set; }
        [Display(Name ="Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name ="Género")]
        public string? Genre { get; set; }
        [Display(Name ="Precio")]
        public decimal Price { get; set; }
        [Display(Name ="Clasificación")]
        public string? Rating { get; set; }           
    }
}
