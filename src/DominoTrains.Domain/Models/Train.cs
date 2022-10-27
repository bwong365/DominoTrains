using DominoTrains.Domain.Exceptions.CustomExceptions;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Domain.Models;

public class Train
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    private List<Domino> _dominoes = new();
    public int EdgeValue { get; private set; }

    public Train(int edgeValue)
    {
        if (edgeValue < Domino.MinValue || edgeValue > Domino.MaxValue)
        {
            throw new GameSetupException($"{nameof(EdgeValue)} must be within the range of allowed values ({Domino.MinValue} - {Domino.MaxValue})");
        }

        _dominoes = new List<Domino>();
        EdgeValue = edgeValue;
    }

    private Train()
    {
    }

    public IReadOnlyCollection<Domino> Dominoes
    {
        get => _dominoes.AsReadOnly();
        private set => _dominoes = value.ToList();
    }

    public bool CanPlay(Domino domino) => domino.A == EdgeValue || domino.B == EdgeValue;

    public void AddDomino(Domino domino)
    {
        if (!CanPlay(domino))
        {
            throw new InvalidGamePlayException("One or both sides of the domino must match the edge of the train");
        }

        if (domino.A == EdgeValue)
        {
            _dominoes.Add(new(domino.A, domino.B));
            EdgeValue = domino.B;
        }
        else
        {
            _dominoes.Add(new(domino.B, domino.A));
            EdgeValue = domino.A;
        }
    }
}