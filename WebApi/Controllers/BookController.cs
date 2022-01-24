using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.DBOperations;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetBookById;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace WebApi.Controllers
{

  [ApiController]
  [Route("[controller]s")]
  public class BookController : ControllerBase
  {

    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public BookController(BookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
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
      GetBooksQuery query = new GetBooksQuery(_context, _mapper);
      var result = query.Handle();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {

      BookByIdViewModel result;
      GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);


      query.BookId = id;

      GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
      validator.ValidateAndThrow(query);
      result = query.Handle();



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

      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      command.Model = newBook;
      CreateBookCommandValidator validator = new CreateBookCommandValidator();

      // Validation error throw to results
      validator.ValidateAndThrow(command);
      command.Handle();

      // ValidationResult result = validator.Validate(command);
      // if(!result.IsValid)
      //   foreach(var item in result.Errors)
      //     Console.WriteLine("Property: {0} \n Error Message: {1}",item.PropertyName, item.ErrorMessage);
      // else
      //   command.Handle();



      return Ok();
    }
    // Put

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {

      UpdateBookCommand command = new UpdateBookCommand(_context);
      command.BookId = id;
      command.Model = updatedBook;
      UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
      validator.ValidateAndThrow(command);

      command.Handle();

      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {

      DeleteBookCommand command = new DeleteBookCommand(_context);

      command.BookId = id;
      DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
      validator.ValidateAndThrow(command);
      command.Handle();


      return Ok();
    }
  }
}