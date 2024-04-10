using MoviesWeb.Models;

namespace MoviesWeb.Services
{
    public interface IMovieServices
    {
        IEnumerable<Movie> QueryAll();

        Movie QueryOne(int? MovieId);

        Movie QueryOneByCond(string keyword);

        IEnumerable<Movie> QueryByCond(string keyword);

        Movie CreateMovie(Movie movie, List<int> SelectedMovies, List<int> SelectedStars);

        Movie UpdateMovie(Movie movie, List<int> SelectedMovies, List<int> SelectedStars);

        Boolean DeleteMovie(int MovieId);
    }
}
