using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcMovies.Data;
using MvcMovies.Models;

namespace MvcMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMoviesContext _context;

        public MoviesController(MvcMoviesContext context)
        {
            _context = context;
        }

        // GET: Movies
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            // Genero
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await GetMovieList(null,null)
            };

            return View(movieGenreVM);
        }
        public async Task<List<Movie>> GetMovieList(string? movieGenre, string? searchString)
        {
            try
            {
                if (_context.Movie == null)
                {
                    throw new Exception("Entity set 'MvcMovieContext.Movie'  is null.");
                }
                var movies = from m in _context.Movie
                             select m;

                if (!String.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(s => s.Title!.Contains(searchString));
                }
                if (!string.IsNullOrEmpty(movieGenre))
                {
                    movies = movies.Where(x => x.Genre == movieGenre);
                }
                return await movies.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Search(string movieGenre, string searchString)
        {
            try
            {
                if (_context.Movie == null)
                {
                    return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
                }
                var movieList = await GetMovieList(movieGenre, searchString);
                return PartialView("_MovieList", movieList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return PartialView("_MovieList",await GetMovieList(null,null));
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //se usa para impedir la falsificacion de una solicitud
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            //if (id != movie.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return PartialView("_MovieList", await GetMovieList(null, null));
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MvcMoviesContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return PartialView("_MovieList", await GetMovieList(null, null));
            //return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
