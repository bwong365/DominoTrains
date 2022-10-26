using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Application.Interfaces;

public interface IDominoesFactory
{
    public List<Domino> CreateDominoes();
}