using CSharpClicker.Domain;

namespace CSharpClicker.UseCases.GetLeaderboard;

public class LeaderboardUserDto
{
	public Guid Id { get; init; }
	public string UserName { get; init; }

	public long RecordScore { get; init; }

	public byte[] Avatar { get; init; } = [];
}
