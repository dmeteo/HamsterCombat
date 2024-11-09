using MediatR;

namespace CSharpClicker.UseCases.Login;

public record LoginCommand(string UserName, string Password) : IRequest<Unit>;
