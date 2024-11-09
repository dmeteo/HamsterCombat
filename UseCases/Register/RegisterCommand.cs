using MediatR;

namespace CSharpClicker.UseCases.Register;

public record RegisterCommand(string UserName, string Password) : IRequest<Unit>;
