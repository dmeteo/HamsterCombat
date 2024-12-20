using MediatR;

namespace CSharpClicker.UseCases.GetUserSettings;

public record GetCurrentUserSettingsQuery : IRequest<UserSettingsDto>;
