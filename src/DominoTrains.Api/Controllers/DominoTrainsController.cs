using AutoMapper;
using DominoTrains.Api.ViewModels;
using DominoTrains.Application.Services.Commands.CreateNewGame;
using DominoTrains.Application.Services.Commands.PlayDomino;
using DominoTrains.Application.Services.Queries.GetGame;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DominoTrains.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DominoTrainsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public DominoTrainsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<GameViewModel>> CreateGameAsync()
    {
        var game = await _sender.Send(new CreateNewGameCommand());
        var gameViewModel = _mapper.Map<GameViewModel>(game);
        return CreatedAtAction(nameof(GetGameAsync), new { gameId = game.Id }, gameViewModel);
    }

    [HttpGet("{gameId:guid}")]
    public async Task<ActionResult<GameViewModel>> GetGameAsync(Guid gameId)
    {
        var game = await _sender.Send(new GetGameQuery { GameId = gameId });
        return _mapper.Map<GameViewModel>(game);
    }

    [HttpPost("{gameId:guid}/playDomino")]
    public async Task<ActionResult<GameViewModel>> PlayDominoAsync(Guid gameId, PlayDominoInputModel inputModel)
    {
        var game = await _sender.Send(_mapper.Map<PlayDominoCommand>((gameId, inputModel)));
        return _mapper.Map<GameViewModel>(game);
    }
}
