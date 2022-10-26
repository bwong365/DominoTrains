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
            throw new InvalidOperationException("Player has no more dominoes to play");
        }

        if (index < 0 || index >= _dominoes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the range of the player's domino list");
        }

        var domino = _dominoes[index];
        if (!train.CanPlay(domino))
        {
            throw new InvalidOperationException("One of the domino's sides must match the train's edge");
        }

        train.AddDomino(domino);
        _dominoes.RemoveAt(index);
    }
}
