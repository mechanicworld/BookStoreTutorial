using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
  public class UpdateAuthorCommand
  {
    public int AuthorId { get; set; }
    public UpdateAuthorModel Model { get; set; }
    private readonly BookStoreDbContext _context;
    public UpdateAuthorCommand(BookStoreDbContext context)
    {
      _context = context;
    }

    public void Handle()
    {
      var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
      if (author is null)
        throw new InvalidOperationException("Author is not in DB");
      if (_context.Authors.Any(x =>
                                    x.Name.ToLower() == Model.Name.ToLower()
                                    && x.Surname.ToLower() == Model.Surname.ToLower()
                                    && x.Id != AuthorId))
      {
        throw new InvalidOperationException("Db has same author");
      }

      author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name: Model.Name;
      author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname: Model.Surname;
      // author.Birthday = Model.Birthday != DateTime.MinValue ? author.Birthday: Model.Birthday;
      _context.SaveChanges();
    }
  }

  public class UpdateAuthorModel
  {
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
  }

}