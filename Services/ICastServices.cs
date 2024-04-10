using MoviesWeb.Models;
namespace MoviesWeb.Services
{
    public interface ICastServices
    {
        IEnumerable<Cast> QueryAll();

        Cast QueryOne(int? StarId);

        Cast QueryOneByCond(string keyword);

        IEnumerable<Cast> QueryByCond(string keyword);

        Cast CreateCast(Cast star, List<int> SelectedStars);

        Cast UpdateCast(Cast star, List<int> SelectedStars);

        Boolean DeleteCast(int StarId);
    }
}
