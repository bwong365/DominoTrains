using AutoMapper;
using DominoTrains.Api.ViewModels;
using DominoTrains.Api.ViewModels.Enums;
using DominoTrains.Application.Services.Commands.PlayDomino;
using DominoTrains.Domain.Aggregates;
using DominoTrains.Domain.Models;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domino, DominoViewModel>();
        CreateMap<Train, TrainViewModel>();
        CreateMap<TrainStation, TrainStationViewModel>();

        CreateMap<Game, GameViewModel>()
            .ForMember(dest => dest.DotsInHand, opt => opt.MapFrom(src => src.Player.GetDotsInHand()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsGameOver() ? GameStatus.Complete : GameStatus.Active))
            .ForMember(dest => dest.Hand, opt => opt.MapFrom(src => src.Player.Dominoes));

        CreateMap<(Guid gameId, PlayDominoInputModel input), PlayDominoCommand>()
            .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.input.Direction))
            .ForMember(dest => dest.DominoIndex, opt => opt.MapFrom(src => src.input.DominoIndex))
            .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.gameId));
    }
}