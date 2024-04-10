using Microsoft.EntityFrameworkCore;
using MoviesWeb.Data;
using MoviesWeb.Models;

namespace MoviesWeb.Services
{

    public class GenreServices : IGenreServices
    {
        private readonly ApplicationDbContext _context;
        public GenreServices(ApplicationDbContext db)
        {
            _context = db;
        }

        public IEnumerable<Genre> QueryAll()
        {
            var genres = _context.Genre
                        .Include(m => m.GenreMovies)
                        .ThenInclude(mg => mg.Movie); ;
            foreach (var genre in genres)
            {
                if (genre.GenreMovies == null)
                {
                    genre.GenreMovies = _context.GenreMovies.Where(c => c.GenreId == genre.Id).ToList();
                    foreach (var movie in genre.GenreMovies)
                    {
                        movie.Movie = _context.Movie.First(c => c.Id == movie.MovieId);
                    }
                }
            }
            return genres;
        }

        public Genre QueryOne(int? GenreId)
        {
            if (GenreId == null)
            {
                return null;
            }
            if (_context.Genre.Any(c => c.Id == GenreId))
            {
                var _genre = _context.Genre.Where(c => c.Id == GenreId)
                            .Include(m => m.GenreMovies)
                            .ThenInclude(mg => mg.Movie)
                            .Single();
                return _genre;
            }
            return null;
        }

        public Genre QueryOneByCond(string keyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Genre> QueryByCond(string keyword)
        {
            if (keyword == "movies")
            {
                var genres = _context.Genre
                        .Include(m => m.GenreMovies)
                        .ThenInclude(mg => mg.Movie);
                return genres;
            }
            return null;
        }

        public Genre CreateGenre(Genre genre, List<int> SelectedMovies)
        {
            if (SelectedMovies != null && SelectedMovies.Any())
            {
                //genre.Id = Guid.NewGuid();
                foreach (var item in SelectedMovies)
                {
                    GenreMovie genreMovie = new GenreMovie();
                    genreMovie.GenreId = genre.Id;
                    genreMovie.MovieId = item;
                    _context.GenreMovies.Add(genreMovie);
                }
            }
            _context.Genre.Add(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre UpdateGenre(Genre genre, List<int> SelectedMovies)
        {
            var _genre = _context.Genre.Where(c => c.Id == genre.Id)
                            .Include(mg => mg.GenreMovies)
                            .ThenInclude(m => m.Movie)
                            .Single();
            if (_genre != null)
            {
                var _genreMovies = _context.GenreMovies.Where(c => c.GenreId == _genre.Id);
                if (_genreMovies.Any())
                {
                    foreach (var movie in _genreMovies)
                    {
                        if (!SelectedMovies.Any(c => c == movie.MovieId))
                        {
                            _context.GenreMovies.Remove(movie);
                        }
                    }
                }
                foreach (var item in SelectedMovies)
                {
                    if (!_genreMovies.Any(c => c.MovieId == item))
                    {
                        GenreMovie genreMovie = new GenreMovie();
                        genreMovie.GenreId = _genre.Id;
                        genreMovie.MovieId = item;
                        _context.GenreMovies.Add(genreMovie);
                    }
                }
                _context.Genre.Update(_genre);
                _context.SaveChanges();
                return _genre;
            }
            return null;
        }

        public bool DeleteGenre(int GenreId)
        {
            if (!_context.Genre.Any(c => c.Id == GenreId))
            {
                return false;
            }
            var genreObj = _context.Genre.Where(c => c.Id == GenreId)
                            .Include(m => m.GenreMovies)
                            .ThenInclude(mg => mg.Movie)
                            .Single();
            if (genreObj != null)
            {
                _context.Genre.Remove(genreObj);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
