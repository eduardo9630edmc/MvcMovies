using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MvcMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage ="El campo Título es requerido",AllowEmptyStrings = false)]
        [StringLength(60,MinimumLength =3,ErrorMessage = "El tamaño del título debe de tener un mínimo de 3 y un máximo de 60 caracteres")]
        public string? Title { get; set; }

        [Display(Name = "Clasificación")]
        [Required(ErrorMessage = "El campo Clasificación es requerido")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string? Rating { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Fecha de lanzamiento")]
        [Required(ErrorMessage = "El campo Fecha de lanzamiento es requerido")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Género")]
        [Required(ErrorMessage = "El campo Género es requerido")]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]      
        public string? Genre { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage ="El campo Precio es requerido")]
        [Range(1,100)]
        [DataType(DataType.Currency)]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }

                 
    }
}
