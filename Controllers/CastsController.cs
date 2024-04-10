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
    public class CastsController : Controller
    {
        private IMovieServices _movieServices;
        private ICastServices _starServices;


        public CastsController(IMovieServices movieServices, ICastServices starServices)
        {
            _movieServices = movieServices;
            _starServices = starServices;
        }

        // GET: Stars
        public async Task<IActionResult> Index()
        {
            var result = _starServices.QueryAll();
            return View(result);
        }

        // GET: Stars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var star = _starServices.QueryOne(id);
            if (star == null)
            {
                return NotFound();
            }

            return View(star);
        }

        // GET: Stars/Create
        public IActionResult Create()
        {
            var StarMovie = _movieServices.QueryByCond("movies");
            ViewBag.StarMovies = StarMovie;
            return View();
        }

        // POST: Stars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cast star, List<int> SelectedStars)
        {
            var StarMovie = _movieServices.QueryByCond("movies");
            ViewBag.StarMovies = StarMovie;
            if (ModelState.IsValid)
            {
                _starServices.CreateCast(star, SelectedStars);
                return RedirectToAction(nameof(Index));
            }
            return View(star);
        }

        // GET: Stars/Edit/5
        public async Task<IActionResult> Edit(int? id, List<int> SelectedStars)
        {
            var StarMovie = _movieServices.QueryByCond("movies");
            ViewBag.StarMovies = StarMovie;

            var star = _starServices.QueryOne(id);
            if (star == null)
            {
                return NotFound();
            }
            return View(star);
        }

        // POST: Stars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cast star, List<int> SelectedStars)
        {
            var StarMovie = _movieServices.QueryByCond("movies");
            ViewBag.StarMovies = StarMovie;
            if (id != star.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = _starServices.UpdateCast(star, SelectedStars);
                }
                catch (Exception) //DbUpdateConcurrencyException
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(star);
        }

        // GET: Stars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var star = _starServices.QueryOne(id);
            if (star == null)
            {
                return NotFound();
            }

            return View(star);
        }

        // POST: Stars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = _starServices.DeleteCast(id);

            if (!result)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
