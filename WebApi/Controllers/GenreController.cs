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
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace WebApi.Controllers
{

  [ApiController]
  [Route("[controller]s")]
  public class GenreController : ControllerBase
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public GenreController(BookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }




    [HttpGet]
    public ActionResult GetGenres()
    {
      GetGenresQuery query = new GetGenresQuery(_context,_mapper);
      var obj = query.Handle();
      return Ok(obj);
    }

    [HttpGet("id")]
    public ActionResult GetGenreDetail(int id)
    {
      GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
      query.GenreId = id;
      GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
      validator.ValidateAndThrow(query);

      var obj = query.Handle();
      return Ok(obj);
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
    {
      CreateGenreCommand command = new CreateGenreCommand(_context);
      command.Model = newGenre;

      CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
      validator.ValidateAndThrow(command);

      command.Handle();
      return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
    {
      UpdateGenreCommand command = new UpdateGenreCommand(_context);
      command.GenreId = id;
      command.Model = updateGenre;

      UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
      validator.ValidateAndThrow(command);

      command.Handle();
      return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteGenre(int id)
    {
      DeleteGenreCommand command = new DeleteGenreCommand(_context);
      command.GenreId = id;

      DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
      validator.ValidateAndThrow(command);

      command.Handle();
      return Ok();
    }
  }

}