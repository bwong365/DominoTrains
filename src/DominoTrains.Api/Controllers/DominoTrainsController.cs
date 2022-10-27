using AutoMapper;
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
        return _mapper.Map<GameViewModel>(game);
    }
}
