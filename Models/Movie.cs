using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MoviesWeb.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public virtual ICollection<GenreMovie>? MovieGenres { get; set; }
        public virtual ICollection<MovieCast>? Stars { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }


        #region Other
        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(1024, MinimumLength = 1)]
        public string Description { get; set; }

        public string? Director { get; set; }

        public string? Writer { get; set; }

        //public string[] Stars { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }

        [DisplayName("IMDB Score")]
        [Precision(4, 2)]
        public decimal IMDBScore { get; set; }

        [DisplayName("MetaCritic Score")]
        [Precision(4, 2)]
        public decimal? MetaScore { get; set; }
        #endregion Other
    }
}
