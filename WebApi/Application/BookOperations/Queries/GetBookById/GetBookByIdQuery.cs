using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;
using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries.GetBookById
{

  public class GetBookByIdQuery
  {
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int BookId { get; set; }

    public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public BookByIdViewModel Handle()
    {
      var book = _dbContext.Books.Include( x=> x.Genre).Where(s => s.Id == BookId).SingleOrDefault();
      if(book is null){
        throw new InvalidOperationException("Book is not in DB");
      }
      BookByIdViewModel vm = _mapper.Map<BookByIdViewModel>(book);  //new BookByIdViewModel();
      // vm.Title = book.Title;
      // vm.PageCount = book.PageCount;
      // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
      // vm.Genre =  ((GenreEnum)book.GenreId).ToString();
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