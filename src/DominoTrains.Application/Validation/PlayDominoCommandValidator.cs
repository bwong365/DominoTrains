using DominoTrains.Application.Services.Commands.PlayDomino;
using FluentValidation;

namespace DominoTrains.Application.Validation;

public class PlayDominoCommandValidator : AbstractValidator<PlayDominoCommand>
{
    public PlayDominoCommandValidator()
    {
        RuleFor(x => x.GameId).NotEmpty();
        RuleFor(x => x.DominoIndex).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Direction)
            .NotNull().WithMessage("Must include a direction")
            .IsInEnum();
    }
}
