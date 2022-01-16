

using AutoMapper;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBooks.GetBooksQuery;

namespace WebApi.Common
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<CreateBookModel, Book>();
      // We want to modify Genre for all member
      CreateMap<Book, BookByIdViewModel>().ForMember(destination => destination.Genre ,option => option.MapFrom(src => ((GenreEnum)src.GenreId).ToString()) );
      CreateMap<Book, BooksViewModel>().ForMember(destination => destination.Genre ,option => option.MapFrom(src => ((GenreEnum)src.GenreId).ToString()) );
    }
  }
}