using Microsoft.EntityFrameworkCore;
using MoviesWeb.Data;
using MoviesWeb.Models;

namespace MoviesWeb.Services
{    public class MovieServices : IMovieServices
    {
        private readonly ApplicationDbContext _context;
        public MovieServices(ApplicationDbContext db)
        {
            _context = db;
        }

        public IEnumerable<Movie> QueryAll()
        {
            var movies = _context.Movie
                        .Include(c => c.Category)
                        .Include(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
                        .Include(c => c.Stars)
                        .ThenInclude(c => c.Cast);
            //foreach (var movie in movies)
            //{
            //    if (movie.MovieGenres == null)
            //    {
            //        movie.MovieGenres = _context.GenreMovies.Where(c => c.MovieId == movie.Id).ToList();
            //        foreach (var genre in movie.MovieGenres)
            //        {
            //            genre.Genre = _context.Genre.First(c => c.Id == genre.GenreId);
            //        }
            //    }
            //}
            return movies;
        }

        public IEnumerable<Movie> QueryByCond(string keyword)
        {
            if (keyword == "movies")
            {
                var movies = _context.Movie
                        .Include(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
                        .Include(c => c.Stars)
                        .ThenInclude(c => c.Cast);

                return movies;
            }
            else
            {
                var result = _context.Movie
                        .Where(c => c.Title.Contains(keyword) || c.Description.Contains(keyword))
                        .Include(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre)
                        .Include(c => c.Stars)
                        .ThenInclude(c => c.Cast);
                return result;
            }
            return null;
        }

        public Movie QueryOne(int? MovieId)
        {
            if (MovieId == null)
            {
                return null;
            }
            if (_context.Movie.Any(c => c.Id == MovieId))
            {
                var movie = _context.Movie.Where(c => c.Id == MovieId)
                            .Include(m => m.MovieGenres)
                            .ThenInclude(mg => mg.Genre)
                            .Single();
                return movie;
            }
            return null;
        }

        public Movie QueryOneByCond(string keyword)
        {
            throw new NotImplementedException();
        }

        public Movie CreateMovie(Movie movie, List<int> SelectedMovies, List<int> SelectedStars)
        {
            //movie.Id = Guid.NewGuid();
            if (SelectedMovies != null && SelectedMovies.Any())
            {
                foreach (var item in SelectedMovies)
                {
                    GenreMovie genreMovie = new GenreMovie();
                    genreMovie.MovieId = movie.Id;
                    genreMovie.GenreId = item;
                    _context.GenreMovies.Add(genreMovie);
                }
            }
            _context.Movie.Add(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie UpdateMovie(Movie movie, List<int> SelectedMovies, List<int> SelectedStars)
        {
            var _movie = _context.Movie.Where(c => c.Id == movie.Id)
                            .Include(mg => mg.MovieGenres)
                            .ThenInclude(g => g.Genre)
                            .Single();
            if (_movie != null)
            {
                //if (SelectedMovies != null && SelectedMovies.Any())
                //    {
                var movieGs = _context.GenreMovies.Where(c => c.MovieId == _movie.Id);
                if (movieGs.Any())
                {
                    foreach (var _genre in movieGs)
                    {
                        if (!SelectedMovies.Any(c => c == _genre.GenreId))
                        {
                            _context.GenreMovies.Remove(_genre);
                        }
                    }
                }

                foreach (var item in SelectedMovies)
                {
                    if (!movieGs.Any(c => c.GenreId == item))
                    {
                        GenreMovie genreMovie = new GenreMovie();
                        genreMovie.MovieId = _movie.Id;
                        genreMovie.GenreId = item;
                        _context.GenreMovies.Add(genreMovie);
                    }
                }



                var _movieStars = _context.MovieCast.Where(c => c.MovieId == _movie.Id);
                if (_movieStars.Any())
                {
                    foreach (var _star in _movieStars)
                    {
                        if (!SelectedStars.Any(c => c == _star.CastId))
                        {
                            _context.MovieCast.Remove(_star);
                        }
                    }
                }

                foreach (var item in SelectedStars)
                {
                    if (!_movieStars.Any(c => c.CastId == item))
                    {
                        MovieCast _movieStar = new MovieCast();
                        _movieStar.MovieId = _movie.Id;
                        _movieStar.CastId = item;
                        _context.MovieCast.Add(_movieStar);
                    }
                }
                //}
                _context.Movie.Update(_movie);
                _context.SaveChanges();
                return _movie;
            }
            return null;
        }

        public bool DeleteMovie(int MovieId)
        {
            if (!_context.Movie.Any(c => c.Id == MovieId))
            {
                return false;
            }
            var movieObj = _context.Movie.Where(c => c.Id == MovieId)
                            .Include(m => m.MovieGenres)
                            .ThenInclude(mg => mg.Genre)
                            .Single();
            if (movieObj != null)
            {
                _context.Movie.Remove(movieObj);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
