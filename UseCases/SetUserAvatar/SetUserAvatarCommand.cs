using MediatR;

namespace CSharpClicker.UseCases.SetUserAvatar;

public record SetUserAvatarCommand(IFormFile Avatar) : IRequest<Unit>;
