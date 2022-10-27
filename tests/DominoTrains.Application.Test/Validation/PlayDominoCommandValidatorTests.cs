using DominoTrains.Application.Services.Commands.PlayDomino;
using DominoTrains.Application.Validation;
using DominoTrains.Domain.Enums;

namespace DominoTrains.Application.Test.Validation;

public class PlayDominoCommandValidatorTests
{
    private readonly PlayDominoCommandValidator _sut;

    public PlayDominoCommandValidatorTests()
    {
        _sut = new PlayDominoCommandValidator();
    }

    [Fact]
    public void IsValid_WhenCommandIsValid()
    {
        var command = new PlayDominoCommand
        {
            GameId = Guid.NewGuid(),
            DominoIndex = 0,
            Direction = Direction.South
        };

        var result = _sut.Validate(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void IsInvalid_WhenGameIdIsEmpty_ReturnsFalse()
    {
        var command = new PlayDominoCommand
        {
            GameId = Guid.Empty,
            DominoIndex = 0,
            Direction = Direction.North
        };

        var result = _sut.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.PropertyName == nameof(command.GameId));
    }

    [Fact]
    public void Result_IsInvalid_WhenDominoIndexIsNegative()
    {
        var command = new PlayDominoCommand
        {
            GameId = Guid.NewGuid(),
            DominoIndex = -1,
            Direction = Direction.North
        };

        var result = _sut.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.PropertyName == nameof(PlayDominoCommand.DominoIndex));
    }
}