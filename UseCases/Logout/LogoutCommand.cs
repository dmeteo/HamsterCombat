using MediatR;

namespace CSharpClicker.UseCases.Logout;

public record LogoutCommand : IRequest<Unit>;
