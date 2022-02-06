using FluentValidation;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
  public class GetAuthorDetailQueryValidator: AbstractValidator<GetAuthorDetailQuery>
  {
    public GetAuthorDetailQueryValidator()
    {
      RuleFor(query => query.AuthorId).GreaterThan(0);
    }
  }
}