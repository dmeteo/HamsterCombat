using CSharpClicker.UseCases.AddPoints;
using CSharpClicker.UseCases.Common;
using CSharpClicker.UseCases.GetBoosts;
using CSharpClicker.UseCases.GetCurrentUser;
using CSharpClicker.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IMediator mediator;

    public HomeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var boosts = await mediator.Send(new GetBoostsQuery());

        var user = await mediator.Send(new GetCurrentUserQuery());

        var viewModel = new IndexViewModel()
        {
            Boosts = boosts,
            User = user,
        };

        return View(viewModel);
    }

    [HttpPost("score")]
    public async Task<ScoreDto> AddToScore(AddPointsCommand command)
        => await mediator.Send(command);
}
