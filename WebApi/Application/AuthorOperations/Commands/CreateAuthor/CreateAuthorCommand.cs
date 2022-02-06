using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
  public class CreateAuthorCommand
  {
    public CreateAuthorModel Model { get; set; }
    private readonly BookStoreDbContext _context;
    public CreateAuthorCommand(BookStoreDbContext context)
    {
      _context = context;
    }

    public void Handle()
    {
      var author = _context.Authors.SingleOrDefault( x => x.Name == Model.Name && x.Surname == Model.Surname && x.Birthday == Model.Birthday);
      if(author is not null)
        throw new InvalidOperationException("Author is already in DB");

        author = new Author();
        author.Name = Model.Name;
        author.Surname = Model.Surname;
        author.Birthday = Model.Birthday;
        _context.Authors.Add(author);
        _context.SaveChanges();
    }
  }

  public class CreateAuthorModel {
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
  }
}