using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoviesWeb.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }


        public virtual ICollection<GenreMovie>? GenreMovies { get; set; }



        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
