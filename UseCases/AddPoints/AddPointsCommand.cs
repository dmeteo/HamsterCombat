using MediatR;

namespace CSharpClicker.UseCases.AddPoints;

public record AddPointsCommand(int Clicks, int Seconds) : IRequest<Unit>;
