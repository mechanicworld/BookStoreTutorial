

using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;

namespace WebApi.Controllers
{

  [ApiController]
  [Route("[controller]s")]
  public class AuthorController : ControllerBase
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public AuthorController(BookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }




    [HttpGet]
    public ActionResult GetAuthor()
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
    public IActionResult AddGenre([FromBody] CreateAuthorModel newAuthor)
    {
      CreateAuthorCommand command = new CreateAuthorCommand(_context);
      command.Model = newAuthor;

      CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
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