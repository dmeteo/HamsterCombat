using MediatR;

namespace CSharpClicker.UseCases.AddPoints;

public record AddPointsCommand(int Times, bool IsAuto = false) : IRequest<Unit>;
