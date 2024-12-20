using MediatR;

namespace CSharpClicker.UseCases.GetUserProfile;

public record GetCurrentUserProfileQuery(Guid id) : IRequest<UserProfileDto>;

