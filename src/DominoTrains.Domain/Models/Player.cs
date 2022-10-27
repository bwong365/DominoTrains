using DominoTrains.Domain.Exceptions.CustomExceptions;
using DominoTrains.Domain.ValueObjects;

namespace DominoTrains.Domain.Models;

public class Player
{
    private List<Domino> _dominoes = new();

    public Player(List<Domino> dominoes)
    {
        _dominoes = dominoes;
    }

    public List<Domino> Dominoes
    {
        get => _dominoes;
        private set => _dominoes = value.ToList();
    }

    private Player()
    {
    }

    public int GetDotsInHand() => _dominoes.Sum(d => d.TotalDots);

    public void PlayDomino(int index, Train train)
    {
        if (_dominoes.Count == 0)
        {
            throw new InvalidGamePlayException("Player has no more dominoes to play");
        }

        if (index < 0 || index >= _dominoes.Count)
        {
            throw new InvalidGamePlayException("The player does not have a domino at the specified index");
        }

        var domino = _dominoes[index];
        if (!train.CanPlay(domino))
        {
            throw new InvalidGamePlayException("One or both sides of the domino must match the edge of the train");
        }

        train.AddDomino(domino);
        _dominoes.RemoveAt(index);
    }
}
