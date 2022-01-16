using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.UpdateBook;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.AddControllers
{

  [ApiController]
  [Route("[controller]s")]
  public class BookController : ControllerBase
  {

    private readonly BookStoreDbContext _context;
    public BookController(BookStoreDbContext context)
    {
      _context = context;
    }

    // Static DB
    // private static List<Book> BookList = new List<Book>()
    // {
    //   new Book{
    //     Id = 1,
    //     Title = "Lean Startup",
    //     GenreId = 1, // Personal Growth
    //     PageCount = 200,
    //     PublishDate = new DateTime(2001,06,12)
    //   },
    //   new Book{
    //     Id = 2,
    //     Title = "Herland",
    //     GenreId = 2, // Science Fiction
    //     PageCount = 250,
    //     PublishDate = new DateTime(2012,06,18)
    //   },
    //   new Book{
    //     Id = 3,
    //     Title = "Dune",
    //     GenreId = 2, // Science Fiction
    //     PageCount = 540,
    //     PublishDate = new DateTime(2001,12,21)
    //   }
    // };


    [HttpGet]
    public IActionResult GetBooks()
    {
      GetBooksQuery query = new GetBooksQuery(_context);
      var result = query.Handle();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {

      BookByIdViewModel result;
      GetBookByIdQuery query = new GetBookByIdQuery(_context);
      try
      {
        query.BookId = id;
        result = query.Handle();

      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }

      return Ok(result);

    }

    // First get also is a get request , because of that system does not allow FromQuery Get method and direct get method
    // at the same time.

    // [HttpGet("{id}")]
    // public Book Get([FromQuery] string id)
    // {

    //   var book = _context.Books.Where(s => s.Id == Convert.ToInt32(id)).SingleOrDefault();
    //   return book;
    // }

    // Post

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {

      CreateBookCommand command = new CreateBookCommand(_context);
      try
      {
        command.Model = newBook;
        command.Handle();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }

      return Ok();
    }
    // Put

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
      try
      {
        UpdateBookCommand command = new UpdateBookCommand(_context);
        command.BookId = id;
        command.Model = updatedBook;
        command.Handle();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }


      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {

      DeleteBookCommand command = new DeleteBookCommand(_context);
      try
      {
        command.BookId = id;
        command.Handle();
      }
      catch (Exception ex)
      {
          
          return BadRequest(ex.Message);
      }
      return Ok();
    }
  }
}