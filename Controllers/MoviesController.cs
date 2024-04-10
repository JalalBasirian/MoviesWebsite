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
    public class MoviesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private IMovieServices _movieServices;
        private IGenreServices _genreServices;
        private ICastServices _starServices;

        //public MoviesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public MoviesController(IMovieServices movieServices, IGenreServices genreServices, ICastServices starServices)
        {
            _movieServices = movieServices;
            _genreServices = genreServices;
            _starServices = starServices;
        }

        // GET: Movies
        //[HttpGet]
        //[Route("Home/Index")]
        public async Task<IActionResult> Index()
        {
            var result = _movieServices.QueryAll();
            //var GenreModel = _genreServices.QueryByCond("movies");
            //ViewData["AllGenres"] = GenreModel.ToList();
            //var genreItems = GenreModel.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Title });
            //ViewBag.Genres = new MultiSelectList(genreItems, "Value", "Text");

            return View(result);
        }

        //[HttpGet]
        //[Route("QueryOneByName/{keyword}")]
        public IActionResult Search(string keyword)
        {
            if (keyword == "" || keyword == null)
            {
                var result = _movieServices.QueryAll();
                return View(result);
            }
            else
            {
                var result = _movieServices.QueryByCond(keyword);
                return View(result);
            }
            return null;
        }

        // GET: Movies/Details/5
        //[HttpGet]
        //[Route("QueryById/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _movieServices.QueryOne(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            var GenreModel = _genreServices.QueryByCond("movies");
            ViewBag.Genre = GenreModel;
            var StarMovie = _starServices.QueryByCond("movies");
            ViewBag.StarMovie = StarMovie;
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie, List<int> SelectedMovies, List<int> SelectedStars)
        {
            var GenreModel = _genreServices.QueryByCond("movies");
            ViewBag.Genre = GenreModel;
            var StarMovie = _starServices.QueryByCond("movies");
            ViewBag.StarMovie = StarMovie;
            if (ModelState.IsValid)
            {
                _movieServices.CreateMovie(movie, SelectedMovies, SelectedStars);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id, List<int>? SelectedMovies, List<int>? SelectedStars)
        {
            var GenreModel = _genreServices.QueryByCond("movies");
            ViewBag.Genre = GenreModel;
            var StarMovie = _starServices.QueryByCond("movies");
            ViewBag.StarMovie = StarMovie;
            var movie = _movieServices.QueryOne(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie, List<int>? SelectedMovies, List<int>? SelectedStars)
        {
            var GenreModel = _genreServices.QueryByCond("movies");
            ViewBag.Genre = GenreModel;
            var StarMovie = _starServices.QueryByCond("movies");
            ViewBag.StarMovie = StarMovie;
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = _movieServices.UpdateMovie(movie, SelectedMovies, SelectedStars);
                }
                catch (Exception) //DbUpdateConcurrencyException
                {
                    throw;
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

            var movie = _movieServices.QueryOne(id);
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
            var result = _movieServices.DeleteMovie(id);

            if (!result)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
