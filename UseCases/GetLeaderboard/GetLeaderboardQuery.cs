using MediatR;

namespace CSharpClicker.UseCases.GetLeaderboard;

public record GetLeaderboardQuery(int Page = 1) : IRequest<LeaderboardDto>;
