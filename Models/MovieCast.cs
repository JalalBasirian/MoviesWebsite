using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesWeb.Models
{
    public class MovieCast
    {
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [ForeignKey("Cast")]
        public int CastId { get; set; }

        public virtual Cast? Cast { get; set; }

        public virtual Movie? Movie { get; set; }
    }
}
