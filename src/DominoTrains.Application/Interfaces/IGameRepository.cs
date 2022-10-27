using DominoTrains.Domain.Aggregates;

namespace DominoTrains.Application.Interfaces;

public interface IGameRepository
{
    public Task AddGameAsync(Game game, CancellationToken? cancellationToken = default);
    public Task<Game?> GetGameAsync(Guid gameId, CancellationToken? cancellationToken = default);
    public Task SaveChangesAsync(CancellationToken? cancellationToken = default);
}