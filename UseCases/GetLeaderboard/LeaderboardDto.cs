namespace CSharpClicker.UseCases.GetLeaderboard;

public class LeaderboardDto
{
	public IReadOnlyCollection<LeaderboardUserDto> Users { get; init; }
}
