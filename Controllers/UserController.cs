using CSharpClicker.UseCases.GetCurrentUser;
using CSharpClicker.UseCases.GetLeaderboard;
using CSharpClicker.UseCases.GetUserProfile;
using CSharpClicker.UseCases.SetUserAvatar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Controllers;

[Route("user")]
[Authorize]
public class UserController : Controller
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("avatar")]
    public async Task<IActionResult> SetAvatar(SetUserAvatarCommand command)
    {
        await mediator.Send(command);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("leaderboard")]
    public async Task<IActionResult> Leaderboard(GetLeaderboardQuery query)
    {
        var leaderboard = await mediator.Send(query);

        return View(leaderboard);
    }

    [HttpGet("settings")]
    public async Task<IActionResult> Settings()
    {
        var userSettings = await mediator.Send(new GetCurrentUserQuery());

        return View(userSettings);
    }

	public async Task<IActionResult> Profile(GetCurrentUserProfileQuery query)
	{
		var userProfile = await mediator.Send(query);

		return View(userProfile);
	}
}
