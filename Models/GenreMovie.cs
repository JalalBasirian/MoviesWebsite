using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesWeb.Models
{
    public class GenreMovie
    {
        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public virtual Movie? Movie { get; set; }

        public virtual Genre? Genre { get; set; }


    }
}
