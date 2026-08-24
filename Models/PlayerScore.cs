namespace gameleaderboardapi.Models;

public class PlayerScore
{
    public Guid Id {get;set;} = Guid.NewGuid();
    public string PlayerName {get;set;} = string.Empty;
    public int Score {get; set;}
    public int WaveSurvived {get;set;}
    public DateTime AchievedAt {get;set;} = DateTime.UtcNow;

}
