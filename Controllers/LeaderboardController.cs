using Microsoft.AspNetCore.Mvc;
using gameleaderboardapi.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gameleaderboardapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly ILeaderboardService _leaderboardService;
    //inject a leaderboard service
    public LeaderboardController(ILeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }
    [HttpPost]
    public async Task<IActionResult> SubmitScore([FromBody] ScoreSubmitDTO DTOscore)
    {
        //validate
        if (string.IsNullOrWhiteSpace(DTOscore.PlayerName) || DTOscore.Score < 0)
        {
            return BadRequest(new {Message = "Invalid Data"});
        }
        //prevent hacker from manually post id or achieved date
        var newScore = new PlayerScore()
        {
            PlayerName = DTOscore.PlayerName,
            WaveSurvived = DTOscore.WaveSurvived,
            Score = DTOscore.Score
            // abc
        };
        await _leaderboardService.AddNewScoreAsync(newScore);
        var response = new {Message = "Added new player score"};
        return Ok(response);
    }
    [HttpGet("tops")]
    public async Task<IActionResult> GetTopLeaderboard([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var topScores = await _leaderboardService.GetTopScoresAsync(pageNumber, pageSize);
        var responseList = topScores.Select(score => new ScoreResponseDTO()
        {
            PlayerName = score.PlayerName,
            Score = score.Score,
            AchievedAt = score.AchievedAt,
            WaveSurvived = score.WaveSurvived
        }).ToList();
        return Ok(responseList);
    }
    [HttpDelete("reset")]
    public async Task<IActionResult> Reset()
    {
        await _leaderboardService.ResetLeaderboardAsync();
        return Ok(new {Message = "Leaderboard Reset"});
    }
}
