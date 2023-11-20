using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
public async Task<IActionResult> Index(string movieGenre, string searchString, string sortOrder)
    {
        // Use LINQ to get list of genres.
        IQueryable<string> genreQuery = from m in _context.Movie
                                        orderby m.Genre
                                        select m.Genre;
        var movies = from m in _context.Movie
                    select m;

        if (!string.IsNullOrEmpty(searchString))
        {
            movies = movies.Where(s => s.Title!.Contains(searchString));
        }

        if (!string.IsNullOrEmpty(movieGenre))
        {
            movies = movies.Where(x => x.Genre == movieGenre);
        }

        ViewData["SearchTitle"] = searchString;
        ViewData["SearchGenre"] = movieGenre;

        ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        ViewData["DateSort"] = sortOrder == "releaseDate" ? "releaseDate_desc" : "releaseDate";
        ViewData["GenreSort"] = sortOrder == "genre" ? "genre_desc" : "genre";
        ViewData["PriceSort"] = sortOrder == "price" ? "price_desc" : "price";
        ViewData["RatingSort"] = sortOrder == "rating" ? "rating_desc" : "rating";

        switch (sortOrder)
        {
            case "title_desc":        
                movies = movies.OrderByDescending(m => m.Title);
                break;
            case "releaseDate":        
            {
                movies = movies.OrderBy(m => m.ReleaseDate);
                break;
            }
            case "releaseDate_desc":        
            {
                movies = movies.OrderByDescending(m => m.ReleaseDate);
                break;
            }

            case "genre":        
            {
                movies = movies.OrderBy(m => m.Genre);
                break;
            }

            case "genre_desc":        
            {
                movies = movies.OrderByDescending(m => m.Genre);
                break;
            }
            case "price":       
            {
                movies = movies.OrderBy(m => m.Price);
                break;
            }

            case "price_desc":       
            {
                movies = movies.OrderByDescending(m => m.Price);
                break;
            }
            case "rating":        
            {
                movies = movies.OrderBy(m => m.Rating);
                break;
            }

            case "rating_desc":        
            {
                movies = movies.OrderByDescending(m => m.Rating);
                break;
            }
            default:
                 movies = movies.OrderBy(m => m.Title);
                 break;

        }
        

        var movieGenreVM = new MovieGenreViewModel
        {
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
            Movies = await movies.ToListAsync()
        };

        return View(movieGenreVM);
    }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id, string? rating)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
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
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                ViewData["Id"] = id;
                return View("../Example/Index");
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

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
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.RemoveRange(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Movies/DeleteAll
        public async Task<IActionResult> DeleteAll()
        {
            return View();
        }

        // POST: Movies/DeleteAll
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAll()
        {
             var allMovies = _context.Movie.ToList();
            if (allMovies.Count > 0) 
            {
                _context.Movie.RemoveRange(allMovies);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSelected(int[] selectedMovies)
        {

            var moviesToBeDeleted = _context.Movie.Where(m => selectedMovies.Contains(m.Id)).ToList();
            if (moviesToBeDeleted.Count > 0) 
            {
                _context.Movie.RemoveRange(moviesToBeDeleted);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
            
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
