using DominoTrains.Application.Interfaces;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Application.Factories;

public class UniqueZeroToSixDominoesFactory : IDominoesFactory
{
    public List<Domino> CreateDominoes()
    {
        var dominoes = new List<Domino>();
        for (var i = Domino.MinValue; i <= Domino.MaxValue; i++)
        {
            for (var j = i; j <= Domino.MaxValue; j++)
            {
                dominoes.Add(new Domino(i, j));
            }
        }
        return dominoes;
    }
}