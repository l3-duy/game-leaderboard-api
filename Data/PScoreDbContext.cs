using Microsoft.EntityFrameworkCore;
using gameleaderboardapi.Models;

namespace gameleaderboardapi.Data;

public class ScoreDbContext: DbContext
{
    public ScoreDbContext(DbContextOptions Options) : base(Options) {

    }

    public DbSet<PlayerScore> PlayerScores {get;set;} // create a new table in db

}

