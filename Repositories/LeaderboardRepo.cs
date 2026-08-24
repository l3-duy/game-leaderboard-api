using gameleaderboardapi.Models;
using gameleaderboardapi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace gameleaderboardapi.Repositories;

public interface ILeaderboardRepo
{
    Task AddAsync(PlayerScore Score);
    Task DeleteAllAsync();
    Task<IEnumerable<PlayerScore>> GetTopAsync(int pageNumber, int pageSize);

}

public class LeaderboardRepo: ILeaderboardRepo
{
    private readonly ScoreDbContext _context;
    public LeaderboardRepo(ScoreDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(PlayerScore Score)
    {
        _context.PlayerScores.Add(Score);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<PlayerScore>> GetTopAsync(int pageNumber, int pageSize)
    {
        return await _context.PlayerScores
        .AsNoTracking()
        .OrderByDescending(s=>s.Score)
        .Skip((pageNumber-1)*pageSize)
        .Take(pageSize)
        .ToListAsync();
    }
    public async Task DeleteAllAsync()
    {
        await _context.PlayerScores.ExecuteDeleteAsync();
    }
}
