using MoviesWeb.Models;

namespace MoviesWeb.Data
{
    public static class DBInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            if (context.Category.Any())
            {
                return;
            }

            var Categories = new Category[]
            {
                new Category { Id = 1, Name = "Movie" },
                new Category { Id = 2, Name = "Serial" },
                new Category { Id = 3, Name = "Mini-Serial" },
                new Category { Id = 4, Name = "Anime" },
                new Category { Id = 5, Name = "Animation" },
                new Category { Id = 6, Name = "Documentary" },
                new Category { Id = 7, Name = "Show" },
                new Category { Id = 8, Name = "Concert" },
                new Category { Id = 9, Name = "Biography" },
                new Category { Id = 10, Name = "Collection" },
                new Category { Id = 11, Name = "Recomended" },
                new Category { Id = 12, Name = "Soon..." }
                //new Category { Id = 13, Name = "" },
                //new Category { Id = 14, Name = "" },
                //new Category { Id = 15, Name = "" },
                //new Category { Id = 16, Name = "" },
                //new Category { Id = 17, Name = "" },
                //new Category { Id = 18, Name = "" },
                //new Category { Id = 19, Name = "" },
                //new Category { Id = 20, Name = "" }
            };
            foreach (Category _cat in Categories)
            {
                await context.Category.AddAsync(_cat);
            }
            await context.SaveChangesAsync();

            var Genres = new Genre[]
            {
                new Genre { Id = 1,Title="Action",Description="Action"},
                new Genre { Id = 2,Title="Adventure",Description="Adventure"},
                new Genre { Id = 3,Title="Sci-Fi",Description="Sci-Fi"},
                new Genre { Id = 4,Title="Thriller",Description="Thriller"},
                new Genre { Id = 5,Title="Crime",Description="Crime"},
                new Genre { Id = 6,Title="Documentary",Description="Documentary"},
                new Genre { Id = 7,Title="Drama",Description="Drama"},
                new Genre { Id = 8,Title="Western",Description="Western"},
                new Genre { Id = 9,Title="History",Description="History"},
                new Genre { Id = 10,Title="War",Description="War"},
                new Genre { Id = 11,Title="Fantasy",Description="Fantasy"},
                new Genre { Id = 12,Title="Mystery",Description="Mystery"},
                new Genre { Id = 13,Title="Comedy",Description="Comedy"},
                new Genre { Id = 14,Title="Romance",Description="Romance"},
                new Genre { Id = 15,Title="Animation",Description="Animation"},
                new Genre { Id = 16,Title="Musical",Description="Musical"},
                new Genre { Id = 17,Title="Family",Description="Family"},
                new Genre { Id = 18,Title="Horror",Description="Horror"},
                new Genre { Id = 19,Title="Suspense",Description="Suspense"}
                //new Genre { Id = 20,Title="",Description=""}
            };
            foreach (Genre _genre in Genres)
            {
                await context.Genre.AddAsync(_genre);
            }
            await context.SaveChangesAsync();


            //var Movies = new Movie[]
            //{
            //    new Movie { Id = 1,Title= "",Description= "" , ReleaseDate= new DateTime(), IMDBScore= 1, MetaScore= 2 }
            //};

        }
    }
}
