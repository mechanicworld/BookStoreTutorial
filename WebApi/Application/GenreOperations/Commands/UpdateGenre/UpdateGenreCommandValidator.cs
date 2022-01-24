using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
  public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
  {
    public UpdateGenreCommandValidator()
    {
      // We are checking the name is min 4 char and with when clase cchecking is not empty at the same time
      RuleFor(command => command.Model.Name).MinimumLength(4).When( x=> x.Model.Name.Trim() != string.Empty);
    }
  }
}