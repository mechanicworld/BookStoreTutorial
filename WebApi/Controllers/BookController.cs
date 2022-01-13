using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace WebApi.AddControllers{

  [ApiController]
  [Route("[controller]s")]
  public class BookController : ControllerBase
  {
    private static List<Book> BookList = new List<Book>()
    {
      new Book{
        Id = 1,
        Title = "Lean Startup",
        GenreId = 1, // Personal Growth
        PageCount = 200,
        PublishDate = new DateTime(2001,06,12)
      },
      new Book{
        Id = 2,
        Title = "Herland",
        GenreId = 2, // Science Fiction
        PageCount = 250,
        PublishDate = new DateTime(2012,06,18)
      },
      new Book{
        Id = 3,
        Title = "Dune",
        GenreId = 2, // Science Fiction
        PageCount = 540,
        PublishDate = new DateTime(2001,12,21)
      }
    };


    [HttpGet]
    public List<Book> GetBooks()
    {
      var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
      return bookList;
    }

    [HttpGet("{id}")]
    public Book GetById( int id)
    {
      
      var book = BookList.Where(s => s.Id == id).SingleOrDefault();
      return book;
    }

    // First get also is a get request , because of that system does not allow FromQuery Get method and direct get method
    // at the same time.
    
    // [HttpGet("{id}")]
    // public Book Get([FromQuery] string id)
    // {
      
    //   var book = BookList.Where(s => s.Id == Convert.ToInt32(id)).SingleOrDefault();
    //   return book;
    // }
  }
}