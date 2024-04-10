using MoviesWeb.Models;

namespace MoviesWeb.Services
{
    public interface IGenreServices
    {
        IEnumerable<Genre> QueryAll();

        Genre QueryOne(int? GenreId);

        Genre QueryOneByCond(string keyword);

        IEnumerable<Genre> QueryByCond(string keyword);

        Genre CreateGenre(Genre genre, List<int> SelectedMovies);

        Genre UpdateGenre(Genre genre, List<int> SelectedMovies);

        Boolean DeleteGenre(int GenreId);
    }
}
