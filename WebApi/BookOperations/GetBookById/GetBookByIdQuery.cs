using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;
using System;

namespace WebApi.BookOperations.GetBooks
{

  public class GetBookByIdQuery
  {
    private readonly BookStoreDbContext _dbContext;
    public int BookId { get; set; }

    public GetBookByIdQuery(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public BookByIdViewModel Handle()
    {
      var book = _dbContext.Books.Where(s => s.Id == BookId).SingleOrDefault();
      if(book is null){
        throw new InvalidOperationException("Book is not in DB");
      }
      BookByIdViewModel vm = new BookByIdViewModel();
      vm.Title = book.Title;
      vm.PageCount = book.PageCount;
      vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
      vm.Genre =  ((GenreEnum)book.GenreId).ToString();
      return vm;
    }
  }

  public class BookByIdViewModel
  {
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
  }
}