namespace gameleaderboardapi.Models;

public class ScoreResponseDTO
{
    public string PlayerName {get;set;} = string.Empty;
    public int Score {get; set;}
    public int WaveSurvived {get;set;}
    public DateTime AchievedAt {get;set;} = DateTime.UtcNow;
}
