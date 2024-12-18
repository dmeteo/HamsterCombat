using MediatR;

namespace CSharpClicker.UseCases.GetLeaderboard;

public class GetLeaderboardQuery : IRequest<LeaderboardDto>;
