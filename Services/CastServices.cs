using Microsoft.EntityFrameworkCore;
using MoviesWeb.Data;
using MoviesWeb.Models;

namespace MoviesWeb.Services
{
    public class CastServices : ICastServices
    {
        private readonly ApplicationDbContext _context;
        public CastServices(ApplicationDbContext db)
        {
            _context = db;
        }

        public IEnumerable<Cast> QueryAll()
        {
            var _stars = _context.Cast
                        .Include(m => m.Movies)
                        .ThenInclude(mg => mg.Movie); ;
            foreach (var star in _stars)
            {
                if (star.Movies == null)
                {
                    star.Movies = _context.MovieCast.Where(c => c.CastId == star.Id).ToList();
                    foreach (var i in star.Movies)
                    {
                        i.Movie = _context.Movie.First(c => c.Id == i.MovieId);
                    }
                }
            }
            return _stars;

        }

        public IEnumerable<Cast> QueryByCond(string keyword)
        {
            if (keyword == "movies")
            {
                var stars = _context.Cast
                        .Include(m => m.Movies)
                        .ThenInclude(mg => mg.Movie);
                return stars;
            }
            return null;
        }

        public Cast QueryOne(int? StarId)
        {
            if (StarId == null)
            {
                return null;
            }
            if (_context.Cast.Any(c => c.Id == StarId))
            {
                var _star = _context.Cast.Where(c => c.Id == StarId)
                            .Include(m => m.Movies)
                            .ThenInclude(mg => mg.Movie)
                            .Single();
                return _star;
            }
            return null;
        }

        public Cast QueryOneByCond(string keyword)
        {
            throw new NotImplementedException();
        }

        public Cast CreateCast(Cast star, List<int> SelectedStars)
        {
            //star.Id = Guid.NewGuid();
            if (SelectedStars != null && SelectedStars.Any())
            {
                foreach (var item in SelectedStars)
                {
                    MovieCast movieStar = new MovieCast();
                    movieStar.CastId = star.Id;
                    movieStar.MovieId = item;
                    _context.MovieCast.Add(movieStar);
                }
            }
            _context.Cast.Add(star);
            _context.SaveChanges();
            return star;
        }

        public Cast UpdateCast(Cast star, List<int> SelectedStars)
        {
            var _star = _context.Cast.Where(c => c.Id == star.Id)
                            .Include(mg => mg.Movies)
                            .ThenInclude(g => g.Movie)
                            .Single();
            if (_star != null)
            {

                var _starMovies = _context.MovieCast.Where(c => c.CastId == _star.Id);
                if (_starMovies.Any())
                {
                    foreach (var i in _starMovies)
                    {
                        if (!SelectedStars.Any(c => c == i.CastId))
                        {
                            _context.MovieCast.Remove(i);
                        }
                    }
                }

                foreach (var item in SelectedStars)
                {
                    if (!_starMovies.Any(c => c.CastId == item))
                    {
                        MovieCast _movieStar = new MovieCast();
                        _movieStar.CastId = _star.Id;
                        _movieStar.MovieId = item;
                        _context.MovieCast.Add(_movieStar);
                    }
                }

                _context.Cast.Update(_star);
                _context.SaveChanges();
                return _star;
            }
            return null;

        }

        public bool DeleteCast(int StarId)
        {
            if (!_context.Cast.Any(c => c.Id == StarId))
            {
                return false;
            }
            var _star = _context.Cast.Where(c => c.Id == StarId)
                            .Include(m => m.Movies)
                            .ThenInclude(mg => mg.Movie)
                            .Single();
            if (_star != null)
            {
                _context.Cast.Remove(_star);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
