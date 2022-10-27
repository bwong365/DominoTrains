using AutoMapper;
using DominoTrains.Api.ViewModels;
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
    }
}