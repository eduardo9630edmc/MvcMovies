using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovies.Models;
using System.Collections.Generic;

namespace MvcMovies.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie>? Movies { get; set; }
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; }
    }
}
