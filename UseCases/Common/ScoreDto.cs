namespace CSharpClicker.UseCases.Common;

public class ScoreDto
{
    public long CurrentScore { get; init; }
    
    public long RecordScore { get; init; }

    public long ProfitPerClick { get; init; }

    public long ProfitPerSecond {  get; init; }
}
