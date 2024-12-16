using MediatR;

namespace CSharpClicker.UseCases.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<UserDto>;
