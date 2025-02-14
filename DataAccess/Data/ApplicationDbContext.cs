using Microsoft.EntityFrameworkCore;
using Models;
namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        // created a connection through this constructor.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // create these tables into the db through DBSet.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        // data seeding.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seeding in Category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "History", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Action", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Latest", DisplayOrder = 3 }
            );

            //seeding in Product
            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "C# Programming Guide",
                Description = "A comprehensive guide to C# programming.",
                ISBN = "978-1234567890",
                Author = "John Doe",
                ListPrice = 49.99,
                Price = 45.99,
                Price50 = 40.99,
                Price100 = 35.99,
                CategoryId = 2,
                ImageUrl = ""
            },
            new Product
            {
                Id = 2,
                Title = "ASP.NET Core Essentials",
                Description = "Learn the essentials of ASP.NET Core.",
                ISBN = "978-0987654321",
                Author = "Jane Smith",
                ListPrice = 59.99,
                Price = 54.99,
                Price50 = 50.99,
                Price100 = 45.99,
                CategoryId = 3,
                ImageUrl = ""
            },
            new Product
            {
                Id = 3,
                Title = "Entity Framework Core in Action",
                Description = "Master Entity Framework Core with real-world examples.",
                ISBN = "978-1122334455",
                Author = "Mark Johnson",
                ListPrice = 39.99,
                Price = 35.99,
                Price50 = 30.99,
                Price100 = 25.99,
                CategoryId = 1,
                ImageUrl = ""
            },
            new Product
            {
                Id = 4,
                Title = "Mastering LINQ",
                Description = "A deep dive into LINQ with practical examples.",
                ISBN = "978-2233445566",
                Author = "Emily Davis",
                ListPrice = 44.99,
                Price = 40.99,
                Price50 = 36.99,
                Price100 = 32.99,
                CategoryId = 3,
                ImageUrl = ""
            },
            new Product
            {
                Id = 5,
                Title = "Blazor WebAssembly Guide",
                Description = "Learn how to build interactive web applications using Blazor WebAssembly.",
                ISBN = "978-3344556677",
                Author = "Michael Brown",
                ListPrice = 69.99,
                Price = 64.99,
                Price50 = 59.99,
                Price100 = 54.99,
                CategoryId = 1,
                ImageUrl = ""
            });
        }
    }
}
