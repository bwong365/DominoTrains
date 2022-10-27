using DominoTrains.Api.ViewModels;
using DominoTrains.Application.Services.Commands.CreateNewGame;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DominoTrains.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DominoTrainsController : ControllerBase
{
    private readonly ISender _sender;

    public DominoTrainsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<GameViewModel>> CreateGameAsync()
    {
        var game = await _sender.Send(new CreateNewGameCommand());
        var gameViewModel = new GameViewModel
        {
            DotsInHand = game.Player.GetDotsInHand(),
            Id = game.Id,
            TrainStation = new TrainStationViewModel
            {
                North = new TrainViewModel { EdgeValue = game.TrainStation.North.EdgeValue, Dominoes = game.TrainStation.North.Dominoes.Select(car => new DominoViewModel { A = car.A, B = car.B }).ToList() },
                East = new TrainViewModel { EdgeValue = game.TrainStation.East.EdgeValue, Dominoes = game.TrainStation.East.Dominoes.Select(car => new DominoViewModel { A = car.A, B = car.B }).ToList() },
                South = new TrainViewModel { EdgeValue = game.TrainStation.South.EdgeValue, Dominoes = game.TrainStation.South.Dominoes.Select(car => new DominoViewModel { A = car.A, B = car.B }).ToList() },
                West = new TrainViewModel { EdgeValue = game.TrainStation.West.EdgeValue, Dominoes = game.TrainStation.West.Dominoes.Select(car => new DominoViewModel { A = car.A, B = car.B }).ToList() }
            },
            Status = game.IsGameOver() ? GameStatus.Complete : GameStatus.Active,
            Hand = game.Player.Dominoes.Select(d => new DominoViewModel { A = d.A, B = d.B }).ToList()
        };

        return gameViewModel;
    }
}
