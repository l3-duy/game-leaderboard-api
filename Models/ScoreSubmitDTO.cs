namespace gameleaderboardapi.Models;

public class ScoreSubmitDTO
{
    public string PlayerName {get;set;} = string.Empty;
    public int Score {get; set;}
    public int WaveSurvived {get;set;}
}
