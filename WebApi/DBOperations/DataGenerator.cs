using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
  public class DataGenerator
  {
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
      {
        if (context.Books.Any())
        {
          return;
        }

        context.Genres.AddRangeAsync(
          new Genre
          {
            Name = "Personal Growth"

          },
            new Genre
            {
              Name = "Science Fiction"

            },
            new Genre
            {
              Name = "Romance"

            }
        );
        context.Books.AddRange(
          new Book
          {
            // Id = 1,
            Title = "Lean Startup",
            GenreId = 1, // Personal Growth
            PageCount = 200,
            PublishDate = new DateTime(2001, 06, 12)
          },
          new Book
          {
            // Id = 2,
            Title = "Herland",
            GenreId = 2, // Science Fiction
            PageCount = 250,
            PublishDate = new DateTime(2012, 06, 18)
          },
          new Book
          {
            // Id = 3,
            Title = "Dune",
            GenreId = 2, // Science Fiction
            PageCount = 540,
            PublishDate = new DateTime(2001, 12, 21)
          }
        );

        context.SaveChanges();
      }
    }
  }
}