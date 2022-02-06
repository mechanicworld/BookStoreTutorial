

using AutoMapper;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;
using static WebApi.Application.AuthorOperations.Queries.GetAuthors.GetAuthorsQuery;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

using static WebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;

namespace WebApi.Common
{ 
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<CreateBookModel, Book>();
      // We want to modify Genre for all member
      CreateMap<Book, BookByIdViewModel>().ForMember(destination => destination.Genre ,option => option.MapFrom(src => src.Genre.Name ));
      CreateMap<Book, BooksViewModel>().ForMember(destination => destination.Genre ,option => option.MapFrom(src => src.Genre.Name ));
      CreateMap<Genre, GenresViewModel>();
      CreateMap<Genre, GenreDetailViewModel>();
      CreateMap<Author, AuthorsViewModel>();
      CreateMap<Author, AuthorDetailViewModel>();
    }
  }
}