using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesWeb.Data;
using MoviesWeb.Models;
using MoviesWeb.Services;

namespace MoviesWeb.Controllers
{
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        private IMovieServices _movieServices;
        private IGenreServices _genreServices;

        public GenresController(IMovieServices movieServices, IGenreServices genreServices)
        {
            _movieServices = movieServices;
            _genreServices = genreServices;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            var _genres = _genreServices.QueryAll();
            return View(_genres);
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = _genreServices.QueryOne(id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            var GenreModel = _movieServices.QueryByCond("movies");
            ViewBag.Movie = GenreModel;
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre, List<int> SelectedMovies)
        {
            var GenreModel = _movieServices.QueryByCond("movies");
            ViewBag.Movie = GenreModel;
            if (ModelState.IsValid)
            {
                _genreServices.CreateGenre(genre, SelectedMovies);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var GenreModel = _movieServices.QueryByCond("movies");
            ViewBag.Movie = GenreModel;
            if (id == null)
            {
                return NotFound();
            }

            var genre = _genreServices.QueryOne(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Genre genre, List<int> SelectedMovies)
        {
            var GenreModel = _movieServices.QueryByCond("movies");
            ViewBag.Movie = GenreModel;
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = _genreServices.UpdateGenre(genre, SelectedMovies);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = _genreServices.QueryOne(id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = _genreServices.DeleteGenre(id);

            if (!result)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
