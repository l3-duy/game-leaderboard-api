using gameleaderboardapi.Models;
using gameleaderboardapi.Data;
using Microsoft.EntityFrameworkCore;
using gameleaderboardapi.Repositories; // for using ToListAsync
public interface ILeaderboardService
{
    Task AddNewScoreAsync(PlayerScore score);
    Task ResetLeaderboardAsync();
    Task<IEnumerable<PlayerScore>> GetTopScoresAsync(int? pageNumber, int? pageSize); 
    // IEnumerable giong nhu interface cua list,array...
    // co the de dang thay the dang enumerable 
}
public class LeaderboardService: ILeaderboardService
{
    //can replace repo if we want in the future
    private readonly ILeaderboardRepo _repo;
    private readonly IConfiguration _config;
    public LeaderboardService(ILeaderboardRepo repo, IConfiguration config)
    {
        _repo = repo; // DI
        _config = config;
    }
    public async Task AddNewScoreAsync(PlayerScore score)
    {
        await _repo.AddAsync(score);
    }
    public async Task ResetLeaderboardAsync()
    {
        await _repo.DeleteAllAsync();
    }
    public async Task<IEnumerable<PlayerScore>> GetTopScoresAsync(int? pageNumber, int? pageSize)
    {
        int validPageNumber = pageNumber ?? 1; // if pageNumber is null then assign it 1
        int defaultSize = _config.GetValue<int>("LeaderboardSettings:DefaultPageSize", 10);
        int validPageSize = pageSize ?? defaultSize;

        return await _repo.GetTopAsync(validPageNumber, validPageSize);
    }
}

