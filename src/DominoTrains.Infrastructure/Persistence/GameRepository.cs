using DominoTrains.Application.Interfaces;
using DominoTrains.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace DominoTrains.Infrastructure.Persistence;

public class GameRepository : IGameRepository
{
    private readonly AppDbContext _context;
    public GameRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Game?> GetGameAsync(Guid gameId, CancellationToken? cancellationToken = null)
    {
        return await _context.Games
            .Include(g => g.TrainStation.North)
            .Include(g => g.TrainStation.East)
            .Include(g => g.TrainStation.South)
            .Include(g => g.TrainStation.West)
            .FirstOrDefaultAsync(g => g.Id == gameId);
    }

    public Task AddGameAsync(Game game, CancellationToken? cancellationToken = null)
    {
        _context.Games.Add(game);
        return _context.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
    }

    public Task SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        return _context.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
    }
}